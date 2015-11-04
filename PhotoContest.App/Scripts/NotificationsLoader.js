$(document).ready(function () {

    var notificationsHub = $.connection.notificationsHub;
    notificationsHub.client.receiveNotification = function (notification) {

        var notificationElement = $("<div>").addClass("alert alert-dismissible alert-info").html("<button type=\"button\" class=\"close\" data-dismiss=\"alert\">×</button>" + notification);
        $("#notifications").append(notificationElement);
    };

    $.connection.hub.start();
});