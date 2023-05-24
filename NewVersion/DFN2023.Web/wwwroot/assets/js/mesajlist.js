document.addEventListener("DOMContentLoaded", () => {

    mesajListGetir(2);
});

var s = 0;
var f = 0;
var datalist;
function mesajListGetir(control) {

    $('#mesajlist').pleaseWait();
    if (control == 1) {

        var s = 0;
        f = f + 5;
    } else {
        s = -1;
        f = -1;
    }
        $.ajax({
            data: {
                start:s,
                finish: f,
            },
            type: "POST",
            url: hst,
            success: function (data) {
                datalist = data;
                console.log(data);
                var a = "";
                $("#mesajlist").html("");
               
                    a = a +' <div class="comment">'
                    if (data.okunmamiscount> 0)
                    {
                        a = a + '<h2><span>' + data.okunmamiscount +'</span> Yeni Mesajınız Var</h2>';
                    }
					 else
                    {

                       a = a + '<h2>Yeni Mesajınız Bulunmamaktadır</h2>'
                    }
               

                    a = a + ' </div>' 
                    if (data.yenigelenmesaj != null) {
                        for (var i = 0; i < data.yenigelenmesaj.length; i++) {
                            if (data.yenigelenmesaj[i].IsShow == false) {
                                a = a + '<li>' +
                                    '<div class="comment-text" style="background-color: aliceblue;">' +
                                    '<p style="font-weight: 600;">' + data.yenigelenmesaj[i].MessageContent + '</p>' +
                                    '<a href="Detay/' + data.yenigelenmesaj[i].FromUser + '"><span>Reply<i class="fa-solid fa-reply"></i></span></a>' +
                                    '</div>' +
                                    '<div class="author-name-comment">' +
                                    '<div>' +
                                    '<h6>' + data.yenigelenmesaj[i].UserFrom + '</h6>' +
                                    '<span>' + data.yenigelenmesaj[i].CreateDate.replace("T", " ").substring(0, 16) + '</span>' +
                                    '</div>' +
                                    '<img alt="man" src="/assets/img/stcontent/50x50.png">' +
                                    '</div>' +
                                    '</li>';
                            }  
                        }
                   
                 
                } else { }
                if (data.mesajlar != null) {
                    if (data.mesajlar.length > 0) {
                        for (var i = 0; i < data.mesajlar.length; i++) {
                            if (data.mesajlar[i].ToUser==user.Id) {
                                a = a + '<li>' +
                                    '<div class="comment-text">' +
                                    '<p>' + data.mesajlar[i].MessageContent + '</p>' +
                                    '<a href="Detay/' + data.mesajlar[i].FromUser + '"><span>Reply<i class="fa-solid fa-reply"></i></span></a>' +
                                    '</div>' +
                                    '<div class="author-name-comment">' +
                                    '<div>' +
                                    '<h6>' + data.mesajlar[i].UserFrom + '</h6>' +
                                    '<span>' + data.mesajlar[i].CreateDate.replace("T", " ").substring(0, 16) + '</span>' +
                                    '</div>' +
                                    '<img alt="man" src="/assets/img/stcontent/50x50.png">' +
                                    '</div>' +
                                    '</li>';
                            } else {
                                a = a + '<li>' +
                                    '<div class="comment-text">' +
                                    '<p>' + data.mesajlar[i].MessageContent + '</p>' +
                                    '<a href="Detay/' + data.mesajlar[i].ToUser + '"><span>Reply<i class="fa-solid fa-reply"></i></span></a>' +
                                    '</div>' +
                                    '<div class="author-name-comment">' +
                                    '<div>' +
                                    '<h6>' + data.mesajlar[i].UserFrom + '</h6>' +
                                    '<span>' + data.mesajlar[i].CreateDate.replace("T", " ").substring(0, 16) + '</span>' +
                                    '</div>' +
                                    '<img alt="man" src="/assets/img/stcontent/50x50.png">' +
                                    '</div>' +
                                    '</li>';
                            }
                               
                             
                        }
                    }
                   
                } else { }
                if (data.tumcount == 0 ) {
                    if (length != -1) {

                    } else {
                        a = a + '<li>' +
                            '<div class="button-gap">' +
                            '<a onclick="mesajListGetir(2)" class="button button-2 non">Mesajları Gizle<i class="fa-solid fa-angle-down"></i></a>' +
                            '</div>' +
                            '</li>';
                    }
                    
                } else {
                    a = a + '<li>' +
                        '<div class="button-gap">' +
                        '<a onclick="mesajListGetir(1)" class="button button-2 non">Diğer Mesajları Gör(' + data.tumcount + ' Mesaj)<i class="fa-solid fa-angle-down"></i></a>' +
                        '</div>' +
                        '</li>';
                }
                   
                    $('#mesajlist').append(a);
              
             
            },
            complete: function (data2) {

                $('#mesajlist').pleaseWait('stop');

            },

        });
    

}

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
