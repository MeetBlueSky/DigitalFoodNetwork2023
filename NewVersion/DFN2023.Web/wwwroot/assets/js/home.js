
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




function seeAll() {
   

        $.ajax({
            dataType: 'json',
            cache: false,
            type: "POST",
            url: hst3,
            success: function (data) {
                console.log(data.hata);
                if (!data.hata) {

                    window.location.href = data.res;
                    
                }
                else {
                    swal.fire({
                        text: data.mesaj,
                        icon: "error",
                        buttonsStyling: false,
                        confirmButtonText: "Ok",
                        customClass: {
                            confirmButton: "btn font-weight-bold btn-light-primary"
                        }
                    }).then(function () {

                    });
                }


            },
            complete: function (data2) {

                $('#exampleModalSizeLg').pleaseWait('stop');

            },
            error: function (data2) {

            }
        });
    }




