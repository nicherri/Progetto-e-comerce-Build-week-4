$(document).ready(function () {
    $('#sidebarCollapse').on('click', function () {
        $('#sidebar').toggleClass('active');
    });

    $(document).on('click', '.product-link', function (e) {
        e.preventDefault();
        var productId = $(this).data('id');
        console.log(`Product link clicked with id: ${productId}`);

        $.ajax({
            url: '/Product/Details',
            type: 'GET',
            data: { id: productId },
            success: function (data) {
                console.log('Product details loaded successfully.');
                $('#modalContent').html(data);
                $('#productModal').modal('show');
            }
        });
    });

    // Funzione per aggiungere al carrello dal modale
    $(document).on('click', '#addToCartBtn', function () {
        var productId = $(this).data('product-id');
        var quantity = $('#modalQuantity').val();

        $.ajax({
            url: '/Cart/AddToCart',
            type: 'POST',
            data: { productId: productId, quantity: quantity },
            success: function (result) {
                if (result.success) {
                    alert('Product added to cart');
                    $('#productModal').modal('hide');
                    updateCartCounter();
                } else {
                    alert('Error adding product to cart');
                }
            }
        });
    });

    // Funzione per aggiornare il contatore del carrello
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

    // Inizializza il contatore del carrello quando il documento è pronto
    updateCartCounter();
});
