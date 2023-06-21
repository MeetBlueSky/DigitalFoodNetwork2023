
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


function favEkleCikar(cid, durum) {
    if (user.length>0) {
        $.ajax({
            data: {
                companyid: cid,
                durum: durum,
            },
            type: "POST",
            url: hst2,
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
var harita = false;
function haritaAc() {

    if (harita) {
        harita = false;

        $("#textyazi").text("Harita Görünümü");
        document.getElementById("urunler").style.display = "block";
        document.getElementById("harita").style.display = "none";
    } else {
        initMap(1);
        harita = true;
        $("#textyazi").text("Liste Görünümü");
        document.getElementById("urunler").style.display = "none";
        document.getElementById("harita").style.display = "block";

    }
  
}
var selectpro = 0;
var loc = [];
var desc = [];
document.addEventListener("DOMContentLoaded", () => {
    $("#textyazi").text("Harita Görünümü");
    $("#kategoriid").val(skate).trigger('change');
    
});
function secKoordinat(id) {
    selectpro = id;
    haritaAc();
    initMap(2);
}

function initMap(control) {

    var map;
    loc = [];
    if (control==1) {
        for (var i = 0; i < maplist.length; i++) {
            loc.push({ lat: parseFloat(maplist[i].MapX), lng: parseFloat(maplist[i].MapY) });
            desc.push({ baslik: maplist[i].BrandName, desc: maplist[i].ShortDescription, unvan: maplist[i].OfficialName, adres: maplist[i].MapAddress, id: maplist[i].Id, userid: maplist[i].UserId, durum: maplist[i].Id, textfav: maplist[i].CompanyId, logo: maplist[i].Logo });

        }
    }
    if (control == 2 && selectpro>0) {
       var map= maplist.filter(x => x.Id == selectpro);
        loc.push({ lat: parseFloat(map[0].MapX), lng: parseFloat(map[0].MapY) });
        desc.push({ baslik: map[0].BrandName, desc: map[0].ShortDescription, unvan: map[0].OfficialName, adres: map[0].MapAddress, id: map[0].Id, userid: map[0].UserId, durum: map[0].Id, textfav: map[0].CompanyId, logo: map[0].Logo });

        
    }

    var center;
    if (loc.length == 1) {
        center = loc[0];
    } else {
        //39.514110953704744, 35.32080729153706
        center = { lat: 39.514110953704744, lng: 35.32080729153706 };
    }
    var myLatLng = center,
        myOptions = {
            zoom: 6.5,
            center: myLatLng,
        },
        map = new google.maps.Map(document.getElementById('map'), myOptions);
    const positions = loc;
    let infowindow
    const markers = positions.map(position => {
        const marker = new google.maps.Marker({
            position,
            map,
        });
        marker.setMap(map)

        return marker
    })
    markers.forEach((marker, index) => {
        marker.addListener("click", () => {
            if (infowindow) infowindow.close()
            let content = `Marker ${index}`
            infowindow = new google.maps.InfoWindow({
                content: desc[index].FavDurum==true?'<div id="content">' +
                    '<div id="siteNotice">' +
                    "</div>" +
                    '<div style="width: 60%;" class="row"><div class="col-4"><img alt="man" src="/assets/logo/' + desc[index].logo + '" style=" width: 100px "></div>'+
                    '<div class="col-6" style="width: inherit;"><a id="firstHeading" href="/Firma/Detay/' + UrlCleanerJS(desc[index].baslik) + '/' + desc[index].id + '" class="firstHeading" style="font-size: 22px;text-decoration: underline;font-weight: 400;cursor: pointer;">' + desc[index].baslik + '</a>' +
                    '<p id="firstHeading" class= "firstHeading" style="font-size: 12px;" > ' + desc[index].unvan + '</p>' +
                    ' <p id="firstHeading" class="firstHeading" style="font-size: 12px;">' + desc[index].desc + '</p>' +
                    '</div><div class= "cafa-button" >' +
                    '<a onclick="favEkleCikar('+desc[index].textfav+',1)"><i class="fas fa-heart-broken"></i> Favoriden Çıkart</a>'+
                    ' <a href="' + hst3+'/' + desc[index].userid + '" > <i class="fa-solid fa-comment-dots"></i> Mesaj Gönder</a >' +
                      '</div ></div > '+
                    
                    '<div id="bodyContent">' +
                    "<p>" + desc[index].adres + "</p></div></div>" :

                    '<div id="content">' +
                    '<div id="siteNotice">' +
                    "</div>" +
                    '<div style="width: 60%;" class="row"><div class="col-4"><img alt="man" src="/assets/logo/' + desc[index].logo + '" style=" width: 100px "></div>' +
                    '<div class="col-6" style="width: inherit;"><a id="firstHeading" href="/Firma/Detay/' + UrlCleanerJS(desc[index].baslik) + '/' + desc[index].id + '" class="firstHeading" style="font-size: 22px;text-decoration: underline;font-weight: 400;cursor: pointer;">' + desc[index].baslik + '</a>' +
                    '<p id="firstHeading" class= "firstHeading" style="font-size: 12px;" > ' + desc[index].unvan + '</p>' +
                    ' <p id="firstHeading" class="firstHeading" style="font-size: 12px;">' + desc[index].desc + '</p>' +
                    '</div><div class= "cafa-button" >' +
                    '	<a onclick="favEkleCikar(' + desc[index].textfav +',0)"><i class="fa-solid fa-heart" style="color: #F29F05;"></i> Favorilere Ekle</a>'+
                    ' <a href="'+hst3+'/'+desc[index].userid+'" > <i class="fa-solid fa-comment-dots"></i> Mesaj Gönder</a >' +
                    '</div ></div > ' +

                    '<div id="bodyContent">' +
                    "<p>" + desc[index].adres + "</p></div></div>",
                ariaLabel: 'AA',
            })
            infowindow.open(map, marker)
        })
    })
}

window.initMap = initMap(1);


function UrlCleanerJS(inputtext) {
    result = inputtext;
    result = result.replace(/ /g, "-");
    result = result.replace(/:/g, "");
    result = result.replace(/ /g, "-");
    result = result.replace(/\//g, "-");
    result = result.replace(/<br>/g, "");
    result = result.replace(/</g, "");
    result = result.replace(/>/g, "")
    result = result.replace(/ı/g, "i");
    result = result.replace(/ş/g, "s");
    result = result.replace(/ö/g, "o");
    result = result.replace(/ü/g, "u");
    result = result.replace(/ğ/g, "g");
    result = result.replace(/ç/g, "c");
    result = result.replace("(", "");
    result = result.replace(")", "");

    return result;
}
