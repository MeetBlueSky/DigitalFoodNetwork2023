

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
        document.getElementById("urunler").style.display = "block";
        document.getElementById("harita").style.display = "none";
    } else {
        harita = true;
        document.getElementById("urunler").style.display = "none";
        document.getElementById("harita").style.display = "block";

    }
  
}

document.addEventListener("DOMContentLoaded", () => {
    for (var i = 0; i < maplist.length; i++) {
        locations = [];
        locations.push(['<b>Name 1</b><br>Address Line 1<br>Bismarck, ND 58501<br>Phone: 701-555-1234<br><a href="#" >Link<a> of some sort.', parseFloat(maplist[i].MapX), parseFloat(maplist[i].MapY), (i+1)]);
    }
});
var locations = [

];

var map = new google.maps.Map(document.getElementById('map'), {
    zoom: 9,
    /* Zoom level of your map */
    center: new google.maps.LatLng(parseFloat(maplist[0].MapX), parseFloat(maplist[0].MapY)),
    /* coordinates for the center of your map */
    mapTypeId: google.maps.MapTypeId.ROADMAP
});

var infowindow = new google.maps.InfoWindow();

var marker, i;

for (i = 0; i < locations.length; i++) {
    marker = new google.maps.Marker({
        position: new google.maps.LatLng(locations[i][1], locations[i][2]),
        map: map
    });

    google.maps.event.addListener(marker, 'click', (function (marker, i) {
        return function () {
            infowindow.setContent(locations[i][0]);
            infowindow.open(map, marker);
        }
    })(marker, i));
}