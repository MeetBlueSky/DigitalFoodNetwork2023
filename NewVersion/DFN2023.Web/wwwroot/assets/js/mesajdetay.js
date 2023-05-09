

function mesajYaz(touser) {
    if (user.length>0) {
        $.ajax({
            data: {
                mesajtext: $("#mesajalani").val(),
                touser: touser,
            },
            type: "POST",
            url: hst,
            success: function (data) {
                if (!data.hata) {

                    swal.fire({
                        text: data.mesaj,
                        icon: "success",
                       
                    });

                    window.location.reload();
                }
                else {
                    Swal.fire(
                        'HATA',
                        data.mesaj,
                        'error'
                    )
                }
             //   window.location.href = '/Tedarikci/List';
            },

        });
    } else {
        girisYapModal();
    }

}
