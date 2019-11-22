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

function switchToRules(){
    navToRulesBtn.addClass("active");
    navToGameBtn.removeClass("active");
    navToEditorBtn.removeClass("active");
    rulesContainer.fadeIn(0);
    gameContainer.fadeOut(0);
    editorContainer.fadeOut(0);
}

function switchToGame(){
    navToRulesBtn.removeClass("active");
    navToGameBtn.addClass("active");
    navToEditorBtn.removeClass("active");
    rulesContainer.fadeOut(0);
    gameContainer.fadeIn(0);
    editorContainer.fadeOut(0);
}

function switchToEditor(){
    navToRulesBtn.removeClass("active");
    navToGameBtn.removeClass("active");
    navToEditorBtn.addClass("active");
    rulesContainer.fadeOut(0);
    gameContainer.fadeOut(0);
    editorContainer.fadeIn(0);
    fillTManagementTable();
}

function fillTManagementTable(){
    $.ajax(getSentences())
    .done(function(result){
        $(managementTable).empty();
        managementTable.append(processEntries(result));
    })
    .fail(function(a, b, c){
        ajaxFail(a, b, c);
    });
}

function processEntries(entries){
    var output = [];
    output.push($('<td><a href="#"><img src="img/Add.png" style="height: 64px;" alt="Add"></img></a></td>'));

    $(entries).each(index => {
        var trElement = $("<tr></tr>");
        trElement.append($('<th class="align-middle" scope="row">' + entries[index].id + '</th>'));
        trElement.append($('<td class="align-middle">' + entries[index].primary + '</td>'));
        trElement.append($('<td class="align-middle">' + entries[index].foreign + '</td>'));
        if(entries[index].description === null){
            trElement.append($('<td class="align-middle">-</td>'));
        }
        else{
            trElement.append($('<td class="align-middle">' + entries[index].description + '</td>'));
        }

        if(entries[index].examplesArray === null){
            trElement.append($('<td class="align-middle">-</td>'));
        }
        else{
            var examples = [];
            $(entries[index].examplesArray).each(descIndex => {
                examples.push($("<span>" + entries[index].examplesArray[descIndex] + "</span>"));
                examples.push($("<br/>"));
            });
            trElement.append($('<td class="align-middle"></td>').append(examples))
        }
        trElement.append('<td class="align-middle">' + entries[index].source + '</td>');
        trElement.append('<td class="align-middle">' + entries[index].subject + '</td>');
        trElement.append('<td class="align-middle"><a href="#"><img src="img/Edit.png" style="height: 64px;" alt="Edit"></img></a></td>');
        trElement.append('<td class="align-middle"><a href="#"><img src="img/Remove.png" style="height: 64px;" alt="Remove"></img></a></td>');

        output.push(trElement);
    });
    output.push($('<td class="align-middle"><a href="#"><img src="img/Add.png" style="height: 64px;" alt="Add"></img></a></td>'));
    return output;
}

var navToRulesBtn;
var navToGameBtn;
var navToEditorBtn;
var rulesContainer;
var gameContainer;
var editorContainer;
var managementTable;
var messageContainer;

$(window).ready(function(){

    navToRulesBtn = $("#navToRules").click(function(){switchToRules()});
    navToGameBtn = $("#navToGame").click(function(){switchToGame()});
    navToEditorBtn = $("#navToEditor").click(function(){switchToEditor()});
    rulesContainer = $("#rulesContainer");
    gameContainer = $("#gameContainer");
    editorContainer = $("#editorContainer");
    managementTable = $("#managementTable");
    messageContainer = $("#messageContainer");
    // $.ajax(login(log, pass)).done(function(result){
    //     $.cookie('token',result);
    //     $.ajaxSetup({
    //         headers:{
    //             'Authorization': $.cookie('token')
    //         }
    //     });
    //     console.log($.cookie('token'));
    //     $.ajax(getSentences()).done(function(result2){
    //         console.log(result2);
    //     }).fail(function(a, b, c){
    //         ajaxFail(a, b, c);
    //     });
    // }).fail(function(a, b, c){
    //     ajaxFail(a, b, c);
    // });
    // $.ajax(getSentence(2)).done(function(result2){
    //     console.log(result2);
    // }).fail(function(a, b, c){
    //     ajaxFail(a, b, c);
    // });
});