$(document).ready(function () {
    console.log("ready!");

    $('.table-remove-button').click(e => removeRowHandler(e));
});

function removeRowHandler(e) {
    let btn = $(e.target);
    let personId = (btn.data('person-id'));

    $.ajax({
        url: "People/Delete?personId=" + personId,
    }).done(() => {
        $(`#tr-person-${personId}`).remove();
    })
}