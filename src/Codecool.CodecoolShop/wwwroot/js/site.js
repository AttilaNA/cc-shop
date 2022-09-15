
// Write your JavaScript code.

init();

function init(){
    makeProductsButtonClickable();
    makeAddToCartButtonsClickable();
    displayCartItemCount();
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
        Button.addEventListener("click", function (e) { addToCart(e.target.dataset.id) });
    }
}
async function addToCart(id) {
    await sendGetRequest(`/Cart/buy/${id}`);
    displayCartItemCount();

}
async function displayCartItemCount() {
    let data = await (await sendGetRequest(`/Cart/item-count`)).json();
    document.querySelector("#CartCount").innerHTML = (data != "0") ? `(${data})` : "";
}

async function decreaseFromCart(id) {
    await sendGetRequest(`/Cart/decrease/${id}`);
    await displayCartItemCount();
}

async function removeFromCart(id) {
    await sendGetRequest(`/Cart/remove/${id}`);
    displayCartItemCount();
}

async function sendGetRequest(url) {
    let response = await fetch(url, {
        method: "GET",
    });
    if (response.ok) {
        return response;
    }
}

$("#checkout-form").validate();
