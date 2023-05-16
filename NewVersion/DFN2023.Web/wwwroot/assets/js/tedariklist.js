
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
        harita = true;
        $("#textyazi").text("Ürünler");
        document.getElementById("urunler").style.display = "none";
        document.getElementById("harita").style.display = "block";

    }
  
}
var loc = [];
var desc = [];
document.addEventListener("DOMContentLoaded", () => {
    $("#textyazi").text("Harita Görünümü");
    $("#kategoriid").val(skate).trigger('change');
    
});

function initMap() {
    const contentString =
        '<div id="content">' +
        '<div id="siteNotice">' +
        "</div>" +
        '<h1 id="firstHeading" class="firstHeading">Uluru</h1>' +
        '<div id="bodyContent">' +
        "<p><b>Uluru</b>, also referred to as <b>Ayers Rock</b>, is a large " +
        "sandstone rock formation in the southern part of the " +
        '<p>Attribution: Uluru, <a href="https://en.wikipedia.org/w/index.php?title=Uluru&oldid=297882194">' +
        "https://en.wikipedia.org/w/index.php?title=Uluru</a> " +
        "(last visited June 22, 2009).</p></div></div>";

    for (var i = 0; i < maplist.length; i++) {
        loc.push({ lat: parseFloat(maplist[i].MapX), lng: parseFloat(maplist[i].MapY) });
        desc.push({ baslik: maplist[i].OfficialName, title: maplist[i].ShortDescription });
  
    }

    var myLatLng = loc[0],
        myOptions = {
            zoom: 11,
            center: myLatLng,
        },
        map = new google.maps.Map(document.getElementById('map'), myOptions)
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
                content: '<div id="content">' +
                    '<div id="siteNotice">' +
                    "</div>" +
                    '<h1 id="firstHeading" class="firstHeading">'+desc[index].baslik+'</h1>' +
                    '<div id="bodyContent">' +
                    "<p><b>Uluru</b>, also referred to as <b>Ayers Rock</b>, is a large " +
                    "sandstone rock formation in the southern part of the " +
                    '<p>Attribution: Uluru, <a href="https://en.wikipedia.org/w/index.php?title=Uluru&oldid=297882194">' +
                    "https://en.wikipedia.org/w/index.php?title=Uluru</a> " +
                    "(last visited June 22, 2009).</p></div></div>",
                ariaLabel: 'AA',
            })
            infowindow.open(map, marker)
        })
    })
}

window.initMap = initMap;

