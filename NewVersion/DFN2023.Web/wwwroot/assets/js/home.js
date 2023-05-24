var recaptcha_response = '';
var response;
function submitUserForm() {
    response = grecaptcha.getResponse();

    if (recaptcha_response.length == 0) {
        document.getElementById('g-recaptcha-error').innerHTML = '<span style="color:red;">Robot değilim onaylanmadı.</span>';
        return false;
    }
    return true;
}

function verifyCaptcha(token) {
    response = grecaptcha.getResponse();

    recaptcha_response = token;
    document.getElementById('g-recaptcha-error').innerHTML = '';
}


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


// Get the <span> element that closes the modal
var rol = 0;
function kayitOl(role) {
    $(':input').val('');
    rol = role;
    kayitmodal.style.display = "block";
}


function kayitOlFunction() {

    response = grecaptcha.getResponse();
    if (response.length > 0) {
    if ($("#kayitpassword").val() != $("#kayitpassword2").val()) {
        uyari("Şifrreler aynı değil");
    }
    else if ($("#kayitname").val().length < 1) {

        uyari("İsim Alanı boş geçilemez");
    }
    else if ($("#kayitsoyad").val().length < 1) {

        uyari("Soyad Alanı boş geçilemez");
    }
    else if ($("#kayittel").val().length < 1) {

        uyari("Telefon Alanı boş geçilemez");
    }
    else if ($("#kayitpassword").val().length < 6) {

        uyari("Şifrreler uzunluğu minimum 6 uzunluğunda olmalıdır");
    }
    else if (document.getElementById("kullanimsartlari").checked == false || document.getElementById("kvkk").checked == false) {

        uyari("Onay kutucuklarını onaylayınız");
    }
    else {
        $('#kayitModal').pleaseWait();
        $.ajax({
            type: "POST",
            async: true,
            url: hst2,
            data: {
                Name: $("#kayitname").val(),
                Surname: $("#kayitsoyad").val(),
                Email: $("#kayitemail").val(),
                CitizenId: $("#kayittckn").val(),
                Phone: $("#kayittel").val(),
                Password: $("#kayitpassword").val(),
                Role: rol,
                Status:0,
            },
            dataType: 'json',
            success: function (data) {
                if (!data.hata) {
                    swal.fire({
                        text: data.mesaj,
                        icon: "success",
                        buttonsStyling: false,
                        confirmButtonText: "Ok",
                        customClass: {
                            confirmButton: "btn font-weight-bold btn-light-primary"
                        }
                    }).then(function () {



                        window.location.href = '/Login/HesapOnayBilgi';



                    });
                } else {
                    uyari(data.mesaj);
                }
                
               
            },
            complete: function (data2) {

                $('#kayitModal').pleaseWait('stop');

            },

        });
    }
}
    else {
    document.getElementById('g-recaptcha-error').innerHTML = '<span style="color:red;">Robot değilim onaylanmadı.</span>';

}

   

}
