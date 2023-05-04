
$(document).ready(function () {
    $('#urunadi').val(ürünadi);
});

function filter() {
    if ($('#kategoriid').val() > 0 && $('#urunadi').val().length>0) {
        $.ajax({
            data: {
                kid: $('#kategoriid').val(),
                urun: $('#urunadi').val(),
            },
            type: "POST",
            url: hst,
            success: function () {


                window.location.href = '/Tedarikci/List';
            },

        });
    }
    
}
