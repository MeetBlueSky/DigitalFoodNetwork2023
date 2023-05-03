
function filter() {
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
