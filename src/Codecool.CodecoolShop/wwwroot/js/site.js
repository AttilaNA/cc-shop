init();

function init() {
    makeAddToCartButtonsClickable();
    displayCartItemCount();
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