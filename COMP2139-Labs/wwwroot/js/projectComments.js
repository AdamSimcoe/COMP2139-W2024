// Function to load comments

function loadComments(projectid) {
    $.ajax({
        url: '/ProjectManagement/ProjectComment/GetComments?projectId=' + projectid,
        method: 'GET',

        success: function (data) {

            var commentsHtml = '';
            for (var i = 0; i < data.length; i++) {
                commentsHtml += '<div class="comment">';
                commentsHtml += '<p>' + data[i].content + '</p>';
                commentsHtml += '<span>Posted on: ' + new Date(data[i].createdDate).toLocaleString() + '</span>';
                commentsHtml += '</div>';
            }
            $('#commentsList').html(commentsHtml);
        }
    });
}

$(document).ready(function () {
    var projectId = $('#projectComments input[name="ProjectId"]').val();

    loadComments(projectId);

    $('#addCommentForm').submit(function (e) {
        e.preventDefault();
        var formData = {
            ProjectId: projectId,
            Content: $('#projectComments textarea[name="Content"]').val()
        };

        $.ajax({
            url: '/ProjectManagement/ProjectComment/AddComment',
            method: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(formData),

            success: function (response) {
                if (response.success) {
                    $('#projectComments textarea[name="Content"]').val('');
                    loadComments(projectId);
                } else {
                    alert(response.message);
                }
            },

            error: function (xhr, status, error) {
                alert("Error: " + error);
            }
        });
    });
});
