var domain = 'https://localhost:44352';
function getSentences(){
    var output = {
        async: true,
        url: domain + "/api/vocabulary",
        data: {},
        type: "GET"
    }; 

    return output;
}

function addSentence(sentence){
    var output = {
        async: true,
        url: domain + "/api/vocabulary",
        data: JSON.stringify(sentence),
        type: "POST"
    }; 

    return output;
}

function updateSentence(sentence){
    var output = {
        async: true,
        url: domain + "/api/vocabulary",
        data: JSON.stringify(sentence),
        type: "PUT",
        contentType: "application/json",
    }; 

    return output;
}

function deleteSentence(id){
    var output = {
        async: true,
        url: domain + "/api/vocabulary/" + id,
        data: {},
        type: "DELETE",
    }; 

    return output;
}

function getSentence(id){
    var output = {
        async: true,
        url: domain + "/api/vocabulary/" + id,
        data: {},
        type: "GET"
    }; 

    return output;
}

function register(login, password){
    var output = {
        async: true,
        url: domain + "/api/auth/register",
        data: JSON.stringify({
            Login: login,
            Password: password
        }),
        type: "POST",
        contentType: "application/json"
    }; 

    return output;
}

function login(login, password){
    var output = {
        async: true,
        url: domain + "/api/auth/login",
        data: JSON.stringify({
            Login: login,
            Password: password
        }),
        contentType: "application/json",
        type: "POST"
    }; 

    return output;
}

function ajaxSuccess(result){
    console.log(result);
}

function ajaxFail(a, b, c){
    console.log(a);
    console.log(b);
    console.log(c);
}

var log = 'Duch003';
var pass = 'Killer003!';
$(window).ready(function(){
    // $.ajax(login(log, pass)).done(function(result){
    //     $.cookie('token',result.outputToken);
    //     $.ajaxSetup({
    //         headers:{
    //             'Authorization': $.cookie('token')
    //         }
    //     });
    //     console.log(result);
    //     $.ajax(getSentences()).done(function(result2){
    //         console.log(result2);
    //     }).fail(function(a, b, c){
    //         ajaxFail(a, b, c);
    //     });
    // }).fail(function(a, b, c){
    //     ajaxFail(a, b, c);
    // });
    $.ajax(getSentences()).done(function(result2){
        console.log(result2);
    }).fail(function(a, b, c){
        ajaxFail(a, b, c);
    });
});