AddEventListenerToRemove();
$('.btn-number').click(function (e) {
    e.preventDefault();

    fieldName = $(this).attr('data-field');
    type = $(this).attr('data-type');
    var input = $("input[name='" + fieldName + "']");
    var currentVal = parseInt(input.val());
    if (!isNaN(currentVal)) {
        if (type == 'minus') {

            if (currentVal > input.attr('min')) {
                input.val(currentVal - 1).change();
                if (document.querySelector('[data-type="plus"]').disabled) {
                    document.querySelector('[data-type="plus"]').disabled = false;
                }
                decreaseFromCart(input[0].dataset.id);
                updateSubTotalPrice(input[0].dataset.id, currentVal - 1);
            }
            if (parseInt(input.val()) == input.attr('min')) {
                $(this).attr('disabled', true);
            }

        } else if (type == 'plus') {

            if (currentVal < input.attr('max')) {
                input.val(currentVal + 1).change();
                if(document.querySelector('[data-type="minus"]').disabled)
                {
                    document.querySelector('[data-type="minus"]').disabled = false;
                }
                addToCart(input[0].dataset.id);
                updateSubTotalPrice(input[0].dataset.id,currentVal + 1);
            }
            if (parseInt(input.val()) == input.attr('max')) {
                $(this).attr('disabled', true);
            }

        }
    } else {
        input.val(0);
    }

});
$('.input-number').focusin(function () {
    $(this).data('oldValue', $(this).val());
});
$('.input-number').change(function (e) {

    minValue = parseInt($(this).attr('min'));
    maxValue = parseInt($(this).attr('max'));
    valueCurrent = parseInt($(this).val());

    name = $(this).attr('name');
    if (valueCurrent >= minValue) {
        $(".btn-number[data-type='minus'][data-field='" + name + "']").removeAttr('disabled');
    } else {
        alert('Sorry, the minimum value was reached');
        $(this).val($(this).data('oldValue'));
    }
    if (valueCurrent <= maxValue) {
        $(".btn-number[data-type='plus'][data-field='" + name + "']").removeAttr('disabled');
    } else {
        alert('Sorry, the maximum value was reached');
        $(this).val($(this).data('oldValue'));
    }

    if (valueCurrent <= maxValue && valueCurrent >= minValue) {
        $(".btn-number[data-type='plus'][data-field='" + name + "']").removeAttr('disabled');
        setItemCount(e.target.dataset.id, valueCurrent);
        updateTotalPrice();
    }

    


});
$(".input-number").keydown(function (e) {
    // Allow: backspace, delete, tab, escape, enter and .
    if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 190]) !== -1 ||
        // Allow: Ctrl+A
        (e.keyCode == 65 && e.ctrlKey === true) ||
        // Allow: home, end, left, right
        (e.keyCode >= 35 && e.keyCode <= 39)) {
        // let it happen, don't do anything
        return;
    }
    // Ensure that it is a number and stop the keypress
    if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
        e.preventDefault();
    }
});


function updateSubTotalPrice(id, count) {
    let span = document.querySelector(`#sub-${id}`);
    span.innerHTML = parseFloat(span.dataset.price) * parseFloat(count);
    updateTotalPrice();

}

function setItemCount(id, count) {
    SetCartItemCount(id, count);
    updateSubTotalPrice(id, count);
}

function AddEventListenerToRemove() {
    const Buttons = document.querySelectorAll(".remove");
    for (let Button of Buttons) {
        Button.addEventListener("click", function(e) {
            removeFromCart(e.target.parentElement.parentElement.dataset.id);
            e.target.parentElement.parentElement.remove();
            updateTotalPrice();
        });
    }
}

function updateTotalPrice() {
    let datas = document.querySelectorAll(".sub-total");
    let sum = 0;
    for (var data of datas) {
        sum += parseFloat(data.innerHTML);
    }
    document.getElementById("total-price").innerHTML = sum;
}