$(document).ready(function () {
    updateCartCounter();

    // Gestisci il cambiamento della quantità
    $('.quantity-input').on('change', function () {
        var cartItemId = $(this).data('id');
        var quantity = $(this).val();
        if (quantity < 1) {
            quantity = 1;  // Imposta il minimo a 1
            $(this).val(quantity);
        }
        updateCartItem(cartItemId, quantity);
    });

    window.updateCartItem = function (cartItemId, quantity) {
        $.ajax({
            url: '/Cart/UpdateCartItem',
            type: 'POST',
            data: { cartItemId: cartItemId, quantity: quantity },
            success: function (result) {
                if (result.success) {
                    location.reload();
                } else {
                    alert('Error updating cart item');
                }
            },
            error: function () {
                alert('Error updating cart item');
            }
        });
    };

    window.deleteCartItem = function (cartItemId) {
        if (confirm('Are you sure you want to delete this item?')) {
            $.ajax({
                url: '/Cart/RemoveFromCart',
                type: 'POST',
                data: { cartItemId: cartItemId },
                success: function (result) {
                    if (result.success) {
                        $('#cart-item-' + cartItemId).remove();
                        updateCartCounter();
                        location.reload();
                    } else {
                        alert('Error deleting cart item');
                    }
                },
                error: function () {
                    alert('Error deleting cart item');
                }
            });
        }
    };

    window.deleteAllCartItems = function () {
        if (confirm('Are you sure you want to delete all items?')) {
            $.ajax({
                url: '/Cart/ClearCart',
                type: 'POST',
                success: function (result) {
                    if (result.success) {
                        location.reload();
                    } else {
                        alert('Error clearing cart');
                    }
                },
                error: function () {
                    alert('Error clearing cart');
                }
            });
        }
    };

    window.addToCart = function (productId, quantity) {
        $.ajax({
            url: '/Cart/AddToCart',
            type: 'POST',
            data: { productId: productId, quantity: quantity },
            success: function (result) {
                if (result.success) {
                    alert('Product added to cart');
                    updateCartCounter();
                } else {
                    alert('Error adding product to cart');
                }
            },
            error: function () {
                alert('Error adding product to cart');
            }
        });
    };

    function updateCartCounter() {
        $.ajax({
            url: '/Cart/GetCartCount',
            method: 'GET',
            success: function (data) {
                var count = data.count;
                if (count > 0) {
                    $('#cart-counter').text(count).show();
                } else {
                    $('#cart-counter').hide();
                }
            },
            error: function () {
                $('#cart-counter').hide();
            }
        });
    }
});
