
function numFormat(p) {

    var n = 2, x = 3;
    var s = '.', c = ',';

    var re = '\\d(?=(\\d{' + (x || 3) + '})+' + (n > 0 ? '\\D' : '$') + ')',
        num = parseFloat(p).toFixed(Math.max(0, ~~n));

    return (c ? num.replace('.', c) : num).replace(new RegExp(re, 'g'), '$&' + (s || ','));
}

function dilne(data) {
    if (data == 1) return 'TR';
    else if (data == 2) return 'EN';
    else return '';
}

function setSessionMe(p1, p2) {
	if (typeof (Storage) !== "undefined") {
		// Store
		sessionStorage.setItem(p1, p2);
	}
}

function getSessionMe(p1) {

	if (typeof (Storage) !== "undefined") {
		// Store
		return sessionStorage.getItem(p1);
	}
	else return '';
}
