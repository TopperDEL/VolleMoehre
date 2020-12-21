///*-----------------------------------------------------------------------------------*/
///*	Volle Möhre
///*-----------------------------------------------------------------------------------*/

function setTabButtons(tabContainer, tabButtons) {
    $(tabContainer).easytabs({
        updateHash: false
    });

    var $tabContainer = $(tabContainer),
        $tabs = $tabContainer.data('easytabs').tabs,
        $tabPanels = $(tabButtons);
    totalSize = $tabPanels.length;

    $tabPanels.each(function (i) {
        if (i != 0) {
            prev = i - 1;
            $(this).prepend("<a href='#' class='prev-tab btn success' rel='" + prev + "'>&laquo; Zurück</a>");
        } else {
            $(this).prepend("<span class='prev-tab btn'>&laquo; Vorherige Seite</span>");
        }
        if (i + 1 != totalSize) {
            next = i + 1;
            $(this).prepend("<a href='#' class='next-tab btn success' rel='" + next + "'>Vor &raquo</a>");
        } else {
            $(this).prepend("<span class='next-tab btn'>Nächste Seite &raquo</span>");
        }
    });

    $('.next-tab, .prev-tab').click(function () {
        var $tabContainer = $(tabContainer),
            $tabs = $tabContainer.data('easytabs').tabs,
            $tabPanels = $(tabButtons);
        var i = parseInt($(this).attr('rel'));
        var tabSelector = $tabs.children('a:eq(' + i + ')').attr('href');
        $tabContainer.easytabs('select', tabSelector);
        return false;
    });
}
