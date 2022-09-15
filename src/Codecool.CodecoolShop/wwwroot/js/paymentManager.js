//paymentSave();

function paymentSave(){
    const paymentButton = document.querySelector("#start-payment");
    paymentButton.addEventListener("click", function (){
       sendGetRequest('/Order/CollectOrderInformation')
    });
}
