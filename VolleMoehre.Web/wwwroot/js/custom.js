/*-----------------------------------------------------------------------------------*/
/*	ANCHOR SCROLL
/*-----------------------------------------------------------------------------------*/
/**
* jQuery.ScrollTo - Easy element scrolling using jQuery.
* Copyright (c) 2007-2009 Ariel Flesler - aflesler(at)gmail(dot)com | http://flesler.blogspot.com
* Dual licensed under MIT and GPL.
* Date: 5/25/2009
* @author Ariel Flesler
* @version 1.4.2
*
* http://flesler.blogspot.com/2007/10/jqueryscrollto.html
*/
; (function (d) { var k = d.scrollTo = function (a, i, e) { d(window).scrollTo(a, i, e) }; k.defaults = { axis: 'xy', duration: parseFloat(d.fn.jquery) >= 1.3 ? 0 : 1 }; k.window = function (a) { return d(window)._scrollable() }; d.fn._scrollable = function () { return this.map(function () { var a = this, i = !a.nodeName || d.inArray(a.nodeName.toLowerCase(), ['iframe', '#document', 'html', 'body']) != -1; if (!i) return a; var e = (a.contentWindow || a).document || a.ownerDocument || a; return d.browser.safari || e.compatMode == 'BackCompat' ? e.body : e.documentElement }) }; d.fn.scrollTo = function (n, j, b) { if (typeof j == 'object') { b = j; j = 0 } if (typeof b == 'function') b = { onAfter: b }; if (n == 'max') n = 9e9; b = d.extend({}, k.defaults, b); j = j || b.speed || b.duration; b.queue = b.queue && b.axis.length > 1; if (b.queue) j /= 2; b.offset = p(b.offset); b.over = p(b.over); return this._scrollable().each(function () { var q = this, r = d(q), f = n, s, g = {}, u = r.is('html,body'); switch (typeof f) { case 'number': case 'string': if (/^([+-]=)?\d+(\.\d+)?(px|%)?$/.test(f)) { f = p(f); break } f = d(f, this); case 'object': if (f.is || f.style) s = (f = d(f)).offset() } d.each(b.axis.split(''), function (a, i) { var e = i == 'x' ? 'Left' : 'Top', h = e.toLowerCase(), c = 'scroll' + e, l = q[c], m = k.max(q, i); if (s) { g[c] = s[h] + (u ? 0 : l - r.offset()[h]); if (b.margin) { g[c] -= parseInt(f.css('margin' + e)) || 0; g[c] -= parseInt(f.css('border' + e + 'Width')) || 0 } g[c] += b.offset[h] || 0; if (b.over[h]) g[c] += f[i == 'x' ? 'width' : 'height']() * b.over[h] } else { var o = f[h]; g[c] = o.slice && o.slice(-1) == '%' ? parseFloat(o) / 100 * m : o } if (/^\d+$/.test(g[c])) g[c] = g[c] <= 0 ? 0 : Math.min(g[c], m); if (!a && b.queue) { if (l != g[c]) t(b.onAfterFirst); delete g[c] } }); t(b.onAfter); function t(a) { r.animate(g, j, b.easing, a && function () { a.call(this, n, b) }) } }).end() }; k.max = function (a, i) { var e = i == 'x' ? 'Width' : 'Height', h = 'scroll' + e; if (!d(a).is('html,body')) return a[h] - d(a)[e.toLowerCase()](); var c = 'client' + e, l = a.ownerDocument.documentElement, m = a.ownerDocument.body; return Math.max(l[h], m[h]) - Math.min(l[c], m[c]) }; function p(a) { return typeof a == 'object' ? a : { top: a, left: a} } })(jQuery);

/**
* jQuery.LocalScroll - Animated scrolling navigation, using anchors.
* Copyright (c) 2007-2009 Ariel Flesler - aflesler(at)gmail(dot)com | http://flesler.blogspot.com
* Dual licensed under MIT and GPL.
* Date: 3/11/2009
* @author Ariel Flesler
* @version 1.2.7
**/
//; (function ($) { var l = location.href.replace(/#.*/, ''); var g = $.localScroll = function (a) { $('body').localScroll(a) }; g.defaults = { duration: 1e3, axis: 'y', event: 'click', stop: true, target: window, reset: true }; g.hash = function (a) { if (location.hash) { a = $.extend({}, g.defaults, a); a.hash = false; if (a.reset) { var e = a.duration; delete a.duration; $(a.target).scrollTo(0, a); a.duration = e } i(0, location, a) } }; $.fn.localScroll = function (b) { b = $.extend({}, g.defaults, b); return b.lazy ? this.bind(b.event, function (a) { var e = $([a.target, a.target.parentNode]).filter(d)[0]; if (e) i(a, e, b) }) : this.find('a,area').filter(d).bind(b.event, function (a) { i(a, this, b) }).end().end(); function d() { return !!this.href && !!this.hash && this.href.replace(this.hash, '') == l && (!b.filter || $(this).is(b.filter)) } }; function i(a, e, b) { var d = e.hash.slice(1), f = document.getElementById(d) || document.getElementsByName(d)[0]; if (!f) return; if (a) a.preventDefault(); var h = $(b.target); if (b.lock && h.is(':animated') || b.onBefore && b.onBefore.call(b, a, f, h) === false) return; if (b.stop) h.stop(true); if (b.hash) { var j = f.id == d ? 'id' : 'name', k = $('<a> </a>').attr(j, d).css({ position: 'absolute', top: $(window).scrollTop(), left: $(window).scrollLeft() }); f[j] = ''; $('body').prepend(k); location = e.hash; k.remove(); f[j] = d } h.scrollTo(f, b).trigger('notify.serialScroll', [f]) } })(jQuery);

$(document).ready(function () {
    $('#menu, .gototop').localScroll();
});

/*-----------------------------------------------------------------------------------*/
/*	SCROLL NAV
/*-----------------------------------------------------------------------------------*/
$(document).ready(function () {
    //Bootstraping variable
    headerWrapper = parseInt($('#header').height());
    offsetTolerance = 40;

    //Detecting user's scroll
    $(window).scroll(function () {

        //Check scroll position
        scrollPosition = parseInt($(this).scrollTop());

        //Move trough each menu and check its position with scroll position then add current class
        $('#menu a').each(function () {

            thisHref = $(this).attr('href');
            if (thisHref.indexOf("facebook") == -1) {
                thisTruePosition = parseInt($(thisHref).offset().top);
                thisPosition = thisTruePosition - headerWrapper - offsetTolerance;

                if (scrollPosition >= thisPosition) {

                    $('.current').removeClass('current');
                    $('#menu a[href=' + thisHref + ']').parent('li').addClass('current');

                }
            }
        });


        //If we're at the bottom of the page, move pointer to the last section
        bottomPage = parseInt($(document).height()) - parseInt($(window).height());

        if (scrollPosition == bottomPage || scrollPosition >= bottomPage) {

            $('.current').removeClass('current');
            $('#menu a:last').parent('li').addClass('current');
        }
    });

});

/*-----------------------------------------------------------------------------------*/
/*	SELECTNAV
/*-----------------------------------------------------------------------------------*/
$(document).ready(function () {

    selectnav('menu', {
        label: '--- Navigation --- ',
        indent: '-'
    });
});

/*-----------------------------------------------------------------------------------*/
/*	SOCIAL ICONS
/*-----------------------------------------------------------------------------------*/
$(document).ready(function () {
    // Add the hover handler to the link
    $("ul.social li a").hover(
		function () {
		    $(this).find("img").animate({ top: '-4px' }, 200);
		},
		function () {
		    $(this).find("img").animate({ top: '0px' }, 200);
		}
	);
});

/*-----------------------------------------------------------------------------------*/
/*	SLIDER
/*-----------------------------------------------------------------------------------*/
$(window).load(function () {
    $('.flexslider').flexslider({
        slideshowSpeed: 4000
    });
});

/*-----------------------------------------------------------------------------------*/
/*	TERMINE CAROUSEL
/*-----------------------------------------------------------------------------------*/
jQuery(document).ready(function () {
    jQuery('#carouselTermine').jcarousel({
        vertical: true,
        scroll: 1
    });
});

/*-----------------------------------------------------------------------------------*/
/*	SPIELER CAROUSEL
/*-----------------------------------------------------------------------------------*/
jQuery(document).ready(function () {
    jQuery('#carouselSpieler').jcarousel({
        vertical: true,
        scroll: 0,
        visible: 10
    });
});

///*-----------------------------------------------------------------------------------*/
///*	Volle Möhre - Nachrichtenpopup
///*-----------------------------------------------------------------------------------*/
$(function () {
    $('.popup-link').click(function () {
        var href = $(this).attr('href');
        $('<div><p class="popup-content" id="popupContent"></p></div>').dialog({
            autoOpen: true,
            modal: true,
            height: 555,
            width: 600,
            open: function () {
                $(this).find('.popup-content').load(href);
            },
            close: function () {
                $(this).dialog('destroy');
            },
            buttons: {
                "Nachricht abschicken": function () {
                    var name = $("input[name=Name]").val();
                    var email = $("input[name=Email]").val();
                    var nachricht = $("textarea[name=Nachricht]").val();
                    var captchastring = $("input[name=captchastring]").val();
                    var kopieAnMich = $('#KopieAnMich').attr('checked') ? true : false;
                    $.ajax({ // create an AJAX call...
                        data: { name: name, email: email, nachricht: nachricht, captchastring: captchastring, kopieAnMich: kopieAnMich, popup: true },
                        type: 'POST', // GET or POST
                        url: '/Kontakt/Nachricht', // the file to call
                        success: function (response) { // on success..
                            if (response.indexOf("success") !== -1) {
                                $('.ui-button:contains(Nachricht)').hide(); //Abschicken-Knopf ausblenden
                            }
                            $('#popupContent').html(response); // update the DIV
                        },
                        error: function (response) { // on success..
                            $('#popupContent').html("<p>Fehler bei der Verbindung zum Server!</p><p>Bitte versuchen Sie es sp\u00e4ter noch einmal.</p>"); // update the DIV
                        }
                    });
                    return false; // cancel original event to prevent form submitting
                    //Umlaute: http://www.weedit.de/clever/programmieren/Wie-kann-man-Umlaute-wie-oe-ue-ae-in-Javascript-darstellen-4Jjvc.php
                },
                "Schlie\u00dfen": function () {
                    $(this).dialog("close");
                }
            }

        });
        return false;
    });
});

//Bilder nachträglich laden
$("img.lazy").lazyload({ effect: "fadeIn", skip_invisible: false });