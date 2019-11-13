function getSentences(){
    var output = {
        async: true,
        url: "https://localhost:44352/api/Vocabulary",
        data: {},
        type: "GET",
    }; 

    return output;
}

function addSentence(sentence){
    var output = {
        async: true,
        url: "/api/vocabulary",
        data: {sentence},
        type: "POST",
        dataType: "json",
    }; 

    return output;
}

function updateSentence(sentence){
    var output = {
        async: true,
        url: "/api/vocabulary",
        data: {sentence},
        type: "PUT",
        dataType: "json"
    }; 

    return output;
}

function deleteSentence(id){
    var output = {
        async: true,
        url: "/api/vocabulary",
        data: {},
        type: "DELETE",
        dataType: "json"
    }; 

    return output;
}

function getSentence(id){
    var output = {
        async: true,
        url: "/api/vocabulary/" + id,
        data: {},
        type: "GET",
        dataType: "json"
    }; 

    return output;
}

$(window).ready(function(){
    console.log($.ajax())
    // $.ajax().done(function(result){
    //     console.log(result);
    // }).fail(function(xhr, stat, err){
    //     console.log(xhr);
    //     console.log(stat);
    //     console.log(err);
    // });
});