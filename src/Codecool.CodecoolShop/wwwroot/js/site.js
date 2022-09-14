// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

init()

function init(){
    makeProductsButtonClickable();
}

function makeProductsButtonClickable(){
    const productButtons = document.querySelectorAll("a.nav-link.text-dark.products");
    for (let productButton of productButtons){
        productButton.addEventListener("click", showMenu);
    }
}

async function showMenu(element){
    let menu = await getProducts(`/Product/${element.target.innerText}`)
    let htmlString = await menu.text()
    let parent = element.target.parentNode;
    parent.innerHTML += htmlString;
}

async function getProducts(url) {
    let response = await fetch(url, {
        method: "GET",
    });
    if (response.ok) {
        return response;
    }
}