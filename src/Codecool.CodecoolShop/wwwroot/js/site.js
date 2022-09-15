init();

function init(){
    makeProductsButtonClickable();
    makeAddToCartButtonsClickable();
    displayCartItemCount();
    proceedToPayment();
   
}

function makeProductsButtonClickable(){
    const productButtons = document.querySelectorAll("a.nav-link.text-dark.products");
    for (let productButton of productButtons){
        productButton.addEventListener("click", showMenu);
    }
}

async function showMenu(element){
    let menu = await sendGetRequest(`/Product/${element.target.innerText}`)
    let htmlString = await menu.text()
    let parent = element.target.nextElementSibling;
    parent.innerHTML = htmlString;
}

function makeAddToCartButtonsClickable() {
    const Buttons = document.querySelectorAll(".add-cart");
    for (let Button of Buttons) {
        Button.addEventListener("click", addToCart);
    }
}

async function addToCart(element) {
    await sendGetRequest(`/Cart/buy/${element.target.dataset.id}`);
    displayCartItemCount();

}
async function displayCartItemCount() {
    let data = await (await sendGetRequest(`/Cart/item-count`)).json();
    document.querySelector("#CartCount").innerHTML = (data != "0") ? `(${data})` : "";
}


async function sendGetRequest(url) {
    let response = await fetch(url, {
        method: "GET",
    });
    if (response.ok) {
        return response;
    }
}

async function sendPostRequest(url, payload) {
    let response = await fetch(url, {
        method: "POST",
        body: payload
    });
    if (response.ok) {
        return response;
    }
}


$("#checkout-form").validate();

function collectCheckoutForm() {
    const checkoutForm = document.querySelector("#checkout");
    let editedForm = new FormData(checkoutForm);
    let billingAddress = editedForm.get('address');
    let city = editedForm.get('city');
    let country = editedForm.get('country');
    let zipcode = editedForm.get('zip');
    billingAddress.concat(', ', country, ', ', city, ', ', zipcode);
    let shippingAddress = editedForm.get('ship_address');
    let shippingCity = editedForm.get('ship_city');
    let shippingCountry = editedForm.get('ship_country');
    let shippingZipcode = editedForm.get('ship_zip');
    shippingAddress.concat(', ', shippingCountry, ', ', shippingCity, ', ', shippingZipcode);
    
    editedForm.set("BillingAddress", billingAddress);
    editedForm.set("ShippingAddress", shippingAddress)
    return editedForm;
}

async function collectOrder() {
    await sendPostRequest('/Order/index', collectCheckoutForm());
}

function proceedToPayment() {
    const paymentButton = document.querySelector("#payment");
    paymentButton.addEventListener("click", collectOrder)
    
}