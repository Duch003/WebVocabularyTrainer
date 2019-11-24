var domain = 'http://localhost:56847';
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
    navToAboutBtn.removeClass("active");
    rulesContainer.fadeIn(0);
    gameContainer.fadeOut(0);
    editorContainer.fadeOut(0);
    aboutContainer.fadeOut(0);
}

function switchToGame(){
    navToRulesBtn.removeClass("active");
    navToGameBtn.addClass("active");
    navToEditorBtn.removeClass("active");
    navToAboutBtn.removeClass("active");
    rulesContainer.fadeOut(0);
    gameContainer.fadeIn(0);
    editorContainer.fadeOut(0);
    aboutContainer.fadeOut(0);
}

function switchToEditor(){
    navToRulesBtn.removeClass("active");
    navToGameBtn.removeClass("active");
    navToEditorBtn.addClass("active");
    navToAboutBtn.removeClass("active");
    rulesContainer.fadeOut(0);
    gameContainer.fadeOut(0);
    editorContainer.fadeIn(0);
    aboutContainer.fadeOut(0);
    fillTManagementTable();
}

function switchToAbout(){
    navToRulesBtn.removeClass("active");
    navToGameBtn.removeClass("active");
    navToEditorBtn.removeClass("active");
    navToAboutBtn.addClass("active");
    rulesContainer.fadeOut(0);
    gameContainer.fadeOut(0);
    editorContainer.fadeOut(0);
    aboutContainer.fadeIn(0);
}

function fillTManagementTable(){
    $.ajax(getSentences())
    .done(function(result){
        $(managementTable).empty();
        managementTable.append(processEntries(result));
        messageContainerPass("Data has been loaded properly.")
    })
    .fail(function(a, b, c){
        messageContainerError("An error has occured while processing query.");
        ajaxFail(a, b, c);
    });
}

function messageContainerError(message){
    messageContainer.text(message);
    messageContainer.removeClass("alert alert-success");
    messageContainer.removeClass("alert alert-primary");
    messageContainer.addClass("alert alert-danger");
}

function messageContainerPass(message){
    messageContainer.text(message);
    messageContainer.removeClass("alert alert-danger");
    messageContainer.removeClass("alert alert-primary");
    messageContainer.addClass("alert alert-success");
}

function messageContainerInfo(message){
    messageContainer.text(message);
    messageContainer.removeClass("alert alert-success");
    messageContainer.removeClass("alert alert-danger");
    messageContainer.addClass("alert alert-primary");
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

function yesNoButtons(){
    return {
        confirm: {
            label: "Yes",
            className: "btn-success"
        },
        cancel: {
            label: "No",
            className: "btn-danger"
        }
    };
    
}

function askQuestion(message, buttons, callback){
    bootbox.confirm({
        message: message,
        buttons: buttons,
        callback: function (result) {
            callback(result);
        }
    });
}

function addEditForm(){
    var form = $(
    `<form>
    <br/>
        <div class="row">
            <div class="col">
                <input type="text" name="Foreign" class="form-control" placeholder="Foreign sentence"/>
            </div>
            <br/>
            <div class="col">
                <input type="text" name="Primary" class="form-control" placeholder="Translation"/>
            </div>
        </div>
        <br/>
        <div class="row">
            <div class="col">
                <input type="text" name="Description" class="form-control" placeholder="Description"/>
            </div>
            <br/>
            <div class="col">
                <input type="text" name="Source" class="form-control" placeholder="Source"/>
            </div>
        </div>
        <br/>
        <div class="row">
            <div class="col">
                <input type="text" name="Subject" class="form-control" placeholder="Subject"/>
            </div>
        </div>
        <br/>
        <div class="row">
            <div class="col">
                <input type="textarea" name="Examples" class="form-control" placeholder="Last name"/>
                <small class="form-text text-muted">Split examples with semicolon.</small>
            </div>
        </div>
    </form>`);

    bootbox.confirm(form,function(){
        
        var primary = form.find('input[name=Primary]').val();
        var foreign = form.find('input[name=Foreign]').val();
        var description = form.find('input[name=Description]').val();
        var source = form.find('input[name=Source]').val();
        var examples = form.find('input[name=Examples]').val();

        var sentence = {
            Primary: primary,
            Foreign: foreign,
            Description: description,
            Source: source,
            Examples: examples
        }
        console.log(primary);
        console.log(foreign);
        console.log(description);
        console.log(source);
        console.log(examples);

        $.ajax(addSentence(sentence))
        .done(function(result){
            fillTManagementTable();
            messageContainerPass("New phrase has been added: " + primary + ": " + foreign);
        }).fail(function(a, b, c){
            messageContainerError("Could not add new phrase.");
            ajaxFail(a, b, c);
        });
    });
}

var navToRulesBtn;
var navToGameBtn;
var navToEditorBtn;
var rulesContainer;
var gameContainer;
var editorContainer;
var managementTable;
var messageContainer;
var navToAboutBtn;
var aboutContainer;

$(window).ready(function(){

    navToRulesBtn = $("#navToRules").click(function(){switchToRules()});
    navToGameBtn = $("#navToGame").click(function(){switchToGame()});
    navToEditorBtn = $("#navToEditor").click(function(){switchToEditor()});
    navToAboutBtn = $("#navToAboutBtn").click(function(){switchToAbout()});
    rulesContainer = $("#rulesContainer");
    aboutContainer = $("#aboutContainer");
    gameContainer = $("#gameContainer");
    editorContainer = $("#editorContainer");
    managementTable = $("#managementTable");
    messageContainer = $("#messageContainer");
    //showBootbox("Test", yesNoButtons(), callback);
    // var html = '<form><div class="form-group"><label for="exampleInputEmail1">Email address</label><input type="email" class="form-control" id="exampleInputEmail1" aria-describedby="emailHelp" placeholder="Enter email"><small id="emailHelp" class="form-text text-muted">Well never share your email with anyone else.</small></div><div class="form-group"><label for="exampleInputPassword1">Password</label><input type="password" class="form-control" id="exampleInputPassword1" placeholder="Password"></div><div class="form-check"><input type="checkbox" class="form-check-input" id="exampleCheck1"><label class="form-check-label" for="exampleCheck1">Check me out</label></div><button type="submit" class="btn btn-primary">Submit</button></form>';
    // bootbox.confirm(html, function(result) {
    //     if(result)
    //         $('#infos').submit();
    // });
    
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