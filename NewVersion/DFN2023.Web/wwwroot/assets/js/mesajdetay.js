﻿var s = 0;
var f = 4;

function mesajDetayGetir(control) {
    if (control == 1) {
    //    s = s + 4;
        f = f + 4;
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
            url: hst2,
            success: function (data) {
                console.log(data);
                var a = "";
                    $("#messarea").html("");
                
                for (var i = 0; i < data.length; i++) {
                    if (data[i].FromUser == user.Id) {
                        if (data[i].IsShow == true) {
                            a = a + '<li>' +
                                '<div class= "comment-text" > ' +
                                ' <p>' + data[i].MessageContent + '</p>' +
                                '	</div> ' +
                                '  <div class= "author-name-comment"> ' +
                                '   <div> ' +
                                ' <h6> Siz</h6 > ' +
                                ' <span>' + data[i].CreateDate.replace("T", " ").substring(0, 16) + '</span > ' +

                                '  <i class="fa fa-check" style = "color: limegreen;" ></i> ' +

                                '   </div> ' +
                                '  <img alt = "man" src = "/assets/img/stcontent/50x50.png"> ' +
                            '</div></li>';
                        } else {
                            a = a + '<li>' +
                                '<div class= "comment-text" > ' +
                                ' <p>' + data[i].MessageContent + '</p>' +
                                '	</div> ' +
                                '  <div class= "author-name-comment"> ' +
                                '   <div> ' +
                                ' <h6> Siz</h6 > ' +
                                ' <span>' + data[i].CreateDate.replace("T", " ").substring(0, 16) + '</span > ' +

                                '  <i class="fa fa-check" style = "color:black" ></i> ' +

                                '   </div> ' +
                                '  <img alt = "man" src = "/assets/img/stcontent/50x50.png"> ' +
                            '</div></li>';
                        }
                       
                    }
                    if (data[i].FromUser != user.Id) {
                        a = a + '<li class="reply one"><div class= "comment-text">' +
                            ' <p>' + data[i].MessageContent + '</p>' +
                            '	</div> ' +
                            '  <div class= "author-name-comment"> ' +
                            '  <img alt = "man" src = "/assets/img/stcontent/50x50.png"> ' +
                            '   <div> ' +
                            ' <h6>' + tedarikciadi + '</h6> ' +
                            ' <span>' + data[i].CreateDate.replace("T", " ").substring(0, 16) + '</span > ' +


                            '   </div> ' +
                            '</div></li>';
                    }
                }

                $('#messarea').append(a);
            },

        });
    

}

function mesajYaz(touser) {
    if (user.Id>0) {
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
