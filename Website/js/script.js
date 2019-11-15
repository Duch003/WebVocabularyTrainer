//https://localhost:44352
function getSentences(){
    var output = {
        async: true,
        url: "https://localhost:44352/api/vocabulary",
        data: {},
        type: "GET"
    }; 

    return output;
}

function addSentence(sentence){
    var output = {
        async: true,
        url: "https://localhost:44352/api/vocabulary",
        data: {sentence},
        type: "POST"
    }; 

    return output;
}

function updateSentence(sentence){
    var output = {
        async: true,
        url: "https://localhost:44352/api/vocabulary",
        data: JSON.stringify(sentence),
        type: "PUT",
        contentType: "application/json",
    }; 

    return output;
}

function deleteSentence(id){
    var output = {
        async: true,
        url: "https://localhost:44352/api/vocabulary/" + id,
        data: {},
        type: "DELETE",
    }; 

    return output;
}

function getSentence(id){
    var output = {
        async: true,
        url: "https://localhost:44352/api/vocabulary/" + id,
        data: {},
        type: "GET"
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

$(window).ready(function(){
    $.ajax(getSentence(3)).done(function(result){
        console.log(result);
        result.Foreign = "Fork";
        $.ajax(updateSentence(result)).done(function(result2){
            console.log(result2)
        }).fail(function(a, b, c){
            console.log(a);
            console.log(b);
            console.log(c);
        });
    });
    $.ajax(getSentences()).done(function(result3){
        console.log(result3);
    }).fail(function(a, b, c){
        console.log(a);
        console.log(b);
        console.log(c);
    });
});