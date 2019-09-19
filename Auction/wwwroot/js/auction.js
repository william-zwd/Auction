"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/auctionHub").build();

//Disable send button until connection is established
document.getElementById("sendButton").disabled = true;

connection.on("ReceiveBidding", function (user, price) {
    var currentdate = new Date(); 
    var datetime = currentdate.getFullYear() + "-"
        + (currentdate.getMonth() + 1) + "-" 
        + currentdate.getDate() + " "
        + currentdate.getHours() + ":"
        + currentdate.getMinutes() + ":"
        + currentdate.getSeconds();
    var row = "<tr><td>" + user + "</td><td>$" + parseFloat(price, 10).toFixed(2) + "</td><td>" + datetime + "</td></tr>";
    $('#auctionHistory tr:last').after(row);
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var user = document.getElementById("userInput").value;
    var price = document.getElementById("priceInput").value;
    connection.invoke("SendBidding", user, price).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});