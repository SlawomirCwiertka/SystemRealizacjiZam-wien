$(document).ready(function () {
    $('i.tree-toggler').click(function () {
        $(this).parent().children('ul.tree').toggle(300);
    });
});
function get(id) {
    $.ajax({
        type: "GET",
        url: "/Products/Products?category=" + id + "&count=" + count,
        dataType: "html",
        success: function (data) {
            if (!data) {
                $('body').html(" ");
            }
            else {
                $('body').html(data);
            }
        },
    })
};
getCount();
function getCount() {
    $.ajax({
        type: "Post",
        url: "/ItemCard/GetItemCardCount",
        success: function (data) {
            $('#itemCard').text(data);
        }
    })
}
function addItem(id) {
    var availbility = parseInt($('#avaibility').val());
    var countItem = parseInt($('#countItem').val());
    if (countItem <= availbility) {
        $.ajax({
            type: "Post",
            url: "/ItemCard/AddItemCard?id=" + id + "&count=" + countItem,
            success: function () {
                availbility -= countItem;
                $('#avaibility').val(availbility);
                getCount();
                window.location = window.location.toString().split("/")[0] + "/Home/Index";
            }
        });
    }
    else {
        alert('Dostępne tylko ' + availbility + 'szt.');
    }
};