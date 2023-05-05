

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


var locations = [
    ['<b>Name 1</b><br>Address Line 1<br>Bismarck, ND 58501<br>Phone: 701-555-1234<br><a href="#" >Link<a> of some sort.', 36.99265338303669, 35.330914331761434, 4],
    ['<b>Name 2</b><br>Address Line 1<br>Fargo, ND 58103<br>Phone: 701-555-4321<br><a href="#" target="_blank">Link<a> of some sort.', 36.99649231333599, 35.34662134856738, 5]
    /*
     * Next point on map
     *   -Notice how the last number within the brackets incrementally increases from the prior marker
     *   -Use http://itouchmap.com/latlong.html to get Latitude and Longitude of a specific address
     *   -Follow the model below:
     *      ['<b>Name 3</b><br>Address Line 1<br>City, ST Zipcode<br>Phone: ###-###-####<br><a href="#" target="_blank">Link<a> of some sort.', ##.####, -##.####, #]
     */
];

var map = new google.maps.Map(document.getElementById('map'), {
    zoom: 9,
    /* Zoom level of your map */
    center: new google.maps.LatLng(36.99265338303669, 35.330914331761434),
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