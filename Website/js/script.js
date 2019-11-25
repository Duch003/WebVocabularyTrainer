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

function getFilteredSentences(filter){
    var output = {
        async: true,
        url: domain + "/api/vocabulary/byFilter?filter=" + filter,
        data: {},
        type: "GET",
        //contentType: "application/xml"
    }; 
    return output;
}

function addSentence(sentence){
    var output = {
        async: true,
        url: domain + "/api/vocabulary",
        data: JSON.stringify(sentence),
        type: "POST",
        contentType: "application/json"
    }; 

    return output;
}

function updateSentence(sentence){
    var output = {
        async: true,
        url: domain + "/api/vocabulary",
        data: JSON.stringify(sentence),
        type: "PUT",
        contentType: "application/json"
    }; 

    return output;
}

function deleteSentence(id){
    var output = {
        async: true,
        url: domain + "/api/vocabulary/" + id,
        data: {},
        type: "DELETE",
        contentType: "application/json"
    }; 
    return output;
}

function getSentence(id){
    var output = {
        async: true,
        url: domain + "/api/vocabulary/byId?id=" + id,
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
    disableSearching();
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
    disableSearching();
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
    fillManagementTable();
    enableSearching();
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
    disableSearching();
}

function fillManagementTable(filter){
    console.log(filter);
    if(!filter){
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
    else{
        $.ajax(getFilteredSentences(filter))
        .done(function(result){
            $(managementTable).empty();
            managementTable.append(processEntries(result));
            messageContainerPass("Current filter: " + filter + ".")
        })
        .fail(function(a, b, c){
            messageContainerError("An error has occured while processing query.");
            ajaxFail(a, b, c);
        });
    }
    
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
    var addButton = $('<tr align="center" class="align-middle"><td colspan="9"><a href="#"><img src="img/Add.png" style="height: 64px;" alt="Add"></img></a></td></tr>');
    var addButton2 = $('<tr align="center" class="align-middle"><td colspan="9"><a href="#"><img src="img/Add.png" style="height: 64px;" alt="Add"></img></a></td></tr>');
    $(addButton).click(function(){
        addForm();
    });
    $(addButton2).click(function(){
        addForm();
    });
    output.push(addButton2);
    $(entries).each(index => {
        var sentence = {
            ID: entries[index].id,
            Primary: entries[index].primary,
            Foreign: entries[index].foreign,
            Description: entries[index].description,
            Source: entries[index].source,
            Examples: entries[index].examples,
            Subject: entries[index].subject
        };
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
        trElement.append($('<td class="align-middle">' + entries[index].source + '</td>'));
        trElement.append($('<td class="align-middle">' + entries[index].subject + '</td>'));
        
        var editButton = $('<td class="align-middle"><a href="#"><img src="img/Edit.png" style="height: 64px;" alt="Edit"></img></a></td>');
        $(editButton).click(function(){
            editForm(sentence);
        })
        
        var removeButton = $('<td class="align-middle"><a href="#"><img src="img/Remove.png" style="height: 64px;" alt="Remove"></img></a></td>');
        $(removeButton).click(function(){
            removeSentence(sentence);
        });
        trElement.append(editButton);
        trElement.append(removeButton);

        output.push(trElement);
    });
    output.push(addButton);
    return output;
}

function removeSentence(sentence){
    bootbox.confirm({
        message: `Are you sure you want to delete given sentence: ${sentence.Primary}: ${sentence.Foreign}?`,
        buttons: {
            confirm: {
                label: "Yes",
                className: "btn-success"
            },
            cancel: {
                label: "No",
                className: "btn-danger"
            }
        },
        callback: function (result) {
            if(result){
                $.ajax(deleteSentence(sentence.ID)).done(function(result){
                    fillManagementTable();
                    messageContainerPass("Sentence has been removed properly.");
                }).fail(function(a, b, c){
                    messageContainerError("An error occured while processing the request.");
                    console.log(a);
                    console.log(b);
                    console.log(c);
                });
            }
        }
    });
}

function editForm(sentence){
    var form = $(
    `<form>
        <br/>
        <div class="row">
            <div class="col">
                <input type="text" name="Foreign" class="form-control" placeholder="Foreign sentence" value="${sentence.Foreign}" />
            </div>
            <br/>
            <div class="col">
                <input type="text" name="Primary" class="form-control" placeholder="Translation" value="${sentence.Primary}"/>
            </div>
        </div>
        <br/>
        <div class="row">
            <div class="col">
                <input type="text" name="Description" class="form-control" placeholder="Description" value="${sentence.Description}"/>
            </div>
            <br/>
            <div class="col">
                <input type="text" name="Source" class="form-control" placeholder="Source" value="${sentence.Source}"/>
            </div>
        </div>
        <br/>
        <div class="row">
            <div class="col">
                <input type="text" name="Subject" class="form-control" placeholder="Subject" value="${sentence.Subject}"/>
            </div>
        </div>
        <br/>
        <div class="row">
            <div class="col">
                <input type="textarea" name="Examples" class="form-control" placeholder="Examples.;Splitted with semicolon." value="${sentence.Examples}"/>
                <small class="form-text text-muted">Split examples with semicolon.</small>
            </div>
        </div>
    </form>`);

    bootbox.confirm(form,function(result){
        if(result === false){
            return;
        }
        var primary = form.find('input[name=Primary]').val();
        var foreign = form.find('input[name=Foreign]').val();
        var description = form.find('input[name=Description]').val();
        var source = form.find('input[name=Source]').val();
        var subject = form.find('input[name=Subject]').val();
        var examples = form.find('input[name=Examples]').val();
        var sentenceToEdit = {
            ID: sentence.ID,
            Primary: primary,
            Foreign: foreign,
            Description: description,
            Source: source,
            Examples: examples,
            Subject: subject
        }

        $.ajax(updateSentence(sentenceToEdit))
        .done(function(result){
            fillManagementTable();
            messageContainerPass("Phrase has been changed: " + primary + ": " + foreign);
        }).fail(function(a, b, c){
            messageContainerError("Could not change phrase.");        
        });
    });
}

function addForm(){
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
                <input type="textarea" name="Examples" class="form-control" placeholder="Examples.;Splitted with semicolon."/>
                <small class="form-text text-muted">Split examples with semicolon.</small>
            </div>
        </div>
    </form>`);

    bootbox.confirm(form,function(){
        
        var primary = form.find('input[name=Primary]').val();
        var foreign = form.find('input[name=Foreign]').val();
        var description = form.find('input[name=Description]').val();
        var source = form.find('input[name=Source]').val();
        var subject = form.find('input[name=Subject]').val();
        var examples = form.find('input[name=Examples]').val();

        var sentence = {
            Primary: primary,
            Foreign: foreign,
            Description: description,
            Source: source,
            Examples: examples,
            Subject: subject
        }

        $.ajax(addSentence(sentence))
        .done(function(result){
            fillManagementTable(result);
            messageContainerPass("New phrase has been added: " + primary + ": " + foreign);
        }).fail(function(a, b, c){
            messageContainerError("Could not add new phrase.");        });
    });
}

function enableSearching(){
    $(searchBar).removeAttr("readonly");
    $(searchSubmit).removeAttr("disabled");
}

function disableSearching(){
    $(searchBar).attr("readonly", "");
    $(searchSubmit).attr("disabled", "disabled");
}

function search(){
    var filter = $(searchBar).val();
    console.log(filter);
    $.ajax(getFilteredSentences(filter)).done(function(result){
        fillManagementTable(result);
        messageContainerInfo("Current filter: " + filter);
    }).fail(function(a, b, c){
        messageContainerError("An error occured while processing query.")
        ajaxFail(a, b, c);
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
var searchBar;
var searchSubmit;

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
    searchBar = $("#searchBar")
    $(searchBar).on('input', function(){
        //search();
        fillManagementTable($(searchBar).val());
    });
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