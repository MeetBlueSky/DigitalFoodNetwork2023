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
     if ($("#kayitname").val().length < 1) {

        uyari("İsim Alanı boş geçilemez");
    }
    else if ($("#kayitsoyad").val().length < 1) {

        uyari("Soyad Alanı boş geçilemez");
    }
    else if ($("#kayittel").val().length < 1) {

        uyari("Telefon Alanı boş geçilemez");
    }

      else if ($("#kayittel").val().length < 13) {

            uyari("Telefon Alanı eksik girildi");
        }
     else if ($("#kayittckn").val().length < 11) {

         uyari("Lütfen TCKN alanını kontrol ediniz.");
        }
    else if (!emailvalid) {

         uyari("Lütfen geçerli bir email adresi giriniz.");
        }
    else if ($("#kayitpassword").val().length < 6) {

        uyari("Şifreler uzunluğu minimum 6 uzunluğunda olmalıdır");
        }
      else  if ($("#kayitpassword").val() != $("#kayitpassword2").val()) {
            uyari("Şifreler uyuşmuyor");
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
                    if (data.res!="/") {
                        window.location.href = data.res;
                    }
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

/*Email Valid*/

const email = document.querySelector('input[name=email]');
const button = document.querySelector('#btn');
const text = document.querySelector('.message');
var emailvalid = false;

const validateEmail = (email) => {
    var regex = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return regex.test(String(email).toLowerCase());
}
document.getElementById("kayitemail").addEventListener("change", validemail);

function validemail() {
    if (validateEmail(email.value)) {
        text.innerText = "Valid email";
        $(".message").removeClass("invalid");
        $(".message").toggleClass("valid");
        emailvalid = true;
        //document.getElementsByClassName('message').style.color = "blue";
    } else {
        text.innerText = "Invalid email";
        $(".message").removeClass("valid");
        $(".message").toggleClass("invalid");
        emailvalid = false;
        //document.getElementsByClassName('message').style.color = "red";
    }
}

/*Email Valid*/


/*Phone Format*/


var zChar = new Array(' ', '(', ')', '-', '.');
var maxphonelength = 13;
var phonevalue1;
var phonevalue2;
var cursorposition;

function ParseForNumber1(object) {
    phonevalue1 = ParseChar(object.value, zChar);
}

function ParseForNumber2(object) {
    phonevalue2 = ParseChar(object.value, zChar);
}

function backspacerUP(object, e) {
    if (e) {
        e = e
    } else {
        e = window.event
    }
    if (e.which) {
        var keycode = e.which
    } else {
        var keycode = e.keyCode
    }

    ParseForNumber1(object)

    if (keycode >= 48) {
        ValidatePhone(object)
    }
}

function backspacerDOWN(object, e) {
    if (e) {
        e = e
    } else {
        e = window.event
    }
    if (e.which) {
        var keycode = e.which
    } else {
        var keycode = e.keyCode
    }
    ParseForNumber2(object)
}

function GetCursorPosition() {

    var t1 = phonevalue1;
    var t2 = phonevalue2;
    var bool = false
    for (i = 0; i < t1.length; i++) {
        if (t1.substring(i, 1) != t2.substring(i, 1)) {
            if (!bool) {
                cursorposition = i
                bool = true
            }
        }
    }
}

function ValidatePhone(object) {

    var p = phonevalue1

    p = p.replace(/[^\d]*/gi, "")

    if (p.length < 3) {
        object.value = p
    } else if (p.length == 3) {
        pp = p;
        d4 = p.indexOf('(')
        d5 = p.indexOf(')')
        if (d4 == -1) {
            pp = "(" + pp;
        }
        if (d5 == -1) {
            pp = pp + ")";
        }
        object.value = pp;
    } else if (p.length > 3 && p.length < 7) {
        p = "(" + p;
        l30 = p.length;
        p30 = p.substring(0, 4);
        p30 = p30 + ")"

        p31 = p.substring(4, l30);
        pp = p30 + p31;

        object.value = pp;

    } else if (p.length >= 7) {
        p = "(" + p;
        l30 = p.length;
        p30 = p.substring(0, 4);
        p30 = p30 + ")"

        p31 = p.substring(4, l30);
        pp = p30 + p31;

        l40 = pp.length;
        p40 = pp.substring(0, 8);
        p40 = p40 + "-"

        p41 = pp.substring(8, l40);
        ppp = p40 + p41;

        object.value = ppp.substring(0, maxphonelength);
    }

    GetCursorPosition()

    if (cursorposition >= 0) {
        if (cursorposition == 0) {
            cursorposition = 2
        } else if (cursorposition <= 2) {
            cursorposition = cursorposition + 1
        } else if (cursorposition <= 5) {
            cursorposition = cursorposition + 2
        } else if (cursorposition == 6) {
            cursorposition = cursorposition + 2
        } else if (cursorposition == 7) {
            cursorposition = cursorposition + 4
            e1 = object.value.indexOf(')')
            e2 = object.value.indexOf('-')
            if (e1 > -1 && e2 > -1) {
                if (e2 - e1 == 4) {
                    cursorposition = cursorposition - 1
                }
            }
        } else if (cursorposition < 11) {
            cursorposition = cursorposition + 3
        } else if (cursorposition == 11) {
            cursorposition = cursorposition + 1
        } else if (cursorposition >= 12) {
            cursorposition = cursorposition
        }

        var txtRange = object.createTextRange();
        txtRange.moveStart("character", cursorposition);
        txtRange.moveEnd("character", cursorposition - object.value.length);
        txtRange.select();
    }

}

function ParseChar(sStr, sChar) {
    if (sChar.length == null) {
        zChar = new Array(sChar);
    } else zChar = sChar;

    for (i = 0; i < zChar.length; i++) {
        sNewStr = "";

        var iStart = 0;
        var iEnd = sStr.indexOf(sChar[i]);

        while (iEnd != -1) {
            sNewStr += sStr.substring(iStart, iEnd);
            iStart = iEnd + 1;
            iEnd = sStr.indexOf(sChar[i], iStart);
        }
        sNewStr += sStr.substring(sStr.lastIndexOf(sChar[i]) + 1, sStr.length);

        sStr = sNewStr;
    }

    return sNewStr;
}
var clipboard = new Clipboard('.btn');

clipboard.on('success', function (e) {
    console.log(e);
});

clipboard.on('error', function (e) {
    console.log(e);
});

/*Phone Format*/



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




