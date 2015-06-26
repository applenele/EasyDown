
var lock = false;
var page = 0;
var key = "";


function LoadResources() {
    if (lock) {
        return;
    }
    else {
        lock = true;
        $.ajax({
            url: "/Resource/getResources",
            type: "post",
            data: { "page": page, "key": key },
        }).done(function (data) {
            var str = "";
            var role = $("#userrole").val();
            if (role == "Admin") {
                for (var i = 0; i < data.length; i++) {
                    str += "<div class='resource_list_one'><div class='resource_list_left'><img src='/Resource/ShowIcon/" + data[i].ID + "' width='72' height='72'></div><div class='resource_list_right'><span><a href='/Resource/Show/" + data[i].ID + "'>" + data[i].ResourceName + "</a></span> <br/><span><a href='/User/Show/" + data[i].UserID + "'>" + data[i].Username + "</a>@" + data[i].Time + "</span><span style='margin-left:20px;'><a href='/Resource/Download/" + data[i].ID + "'>下载</a></span><span style='margin-left:20px;'><a href='javascript:void(0)' class='deleteR'>删除<input type='hidden' value='" + data[i].ID + "'/></a></span></div></div><div class='clr'></div>";
                }
            }
            else {

                for (var i = 0; i < data.length; i++) {
                    str += "<div class='resource_list_one'><div class='resource_list_left'><img src='/Resource/ShowIcon/" + data[i].ID + "' width='72' height='72'></div><div class='resource_list_right'><span><a href='/Resource/Show/" + data[i].ID + "'>" + data[i].ResourceName + "</a></span><br/><span><a href='/User/Show/" + data[i].UserID + "'>" + data[i].Username + "</a>@" + data[i].Time + "</span><span style='margin-left:20px;'><a href='/Resource/Download/" + data[i].ID + "'>下载</a></span></div><div class='clr'></div></div>";
                }

                //for (var i = 0; i < data.length; i++) {
                //    str += "<li><span><a href='/Resource/Show/" + data[i].ID + "'>" + data[i].ResourceName + "</a></span><span style='margin-left:20px;'><a href='/User/Show/" + data[i].UserID + "'>" + data[i].Username + "</a>@" + data[i].Time + "</span><span style='margin-left:20px;'><a href='/Resource/Download/" + data[i].ID + "'>下载</a></span></li>";
                //}
            }
            $("#resourceLst").append(str);

            $(".deleteR").click(function () {
                var id = $(this).children("input").val();
                $.post("/Resource/Delete/" + id, function (data) {
                    if (data == "ok") {
                        alert("删除成功！");
                        lock = false;
                        page = 0;
                        key = "";
                        $("#resourceLst").html("");
                        Load();
                        return;
                    } else {
                        alert("删除失败！");
                        lock = false;
                        page = 0;
                        key = "";
                        $("#resourceLst").html("");
                        Load();
                        return;
                    }
                })
            });

            if (data.length == 10) {
                lock = false;
                page++;
            }
        });
    }
}

function Load() {
    if ($("#resourceLst").length > 0) {
        LoadResources();
    }  
}

function ShowNew(data) {
    var str = "";
    var role = $("#userrole").val();
    if (role == "Admin") {
        str += "<li><span><a href='/Resource/Show/" + data.ID + "'>" + data.ResourceName + "</a></span><span style='margin-left:20px;'><a href='/User/Show/" + data.UserID + "'>" + data.Username + "</a>@" + data.Time + "</span><span style='margin-left:20px;'><a href='/Resource/Download/" + data.ID + "'>下载</a></span><span style='margin-left:20px;'><a href='javascript:void(0)' class='deleteR'>删除<input type='hidden' value='" + data.ID + "'/></a></span></li>";
    }
    else {
        str += "<li><span><a href='/Resource/Show/" + data.ID + "'>" + data.ResourceName + "</a></span><span style='margin-left:20px;'><a href='/User/Show/" + data.UserID + "'>" + data.Username + "</a>@" + data.Time + "</span><span style='margin-left:20px;'><a href='/Resource/Download/" + data.ID + "'>下载</a></span></li>";
    }
    $("#resourceLst").prepend(str);
    $(".deleteR").click(function () {
        var id = $(this).children("input").val();
        $.post("/Resource/Delete/" + id, function (data) {
            if (data == "ok") {
                alert("删除成功！");
                lock = false;
                page = 0;
                key = "";
                $("#resourceLst").html("");
                Load();
                return;
            } else {
                alert("删除失败！");
                lock = false;
                page = 0;
                key = "";
                $("#resourceLst").html();
                Load();
                return;
            }
        })
    });
}

$(document).ready(function () {

    Load();

    $(window).scroll(
     function () {
         totalheight = parseFloat($(window).height())
            + parseFloat($(window).scrollTop());
         if ($(document).height() <= totalheight) {
             Load();
         }
     });

    $("#btnSearch").click(function () {
        $("#resourceLst").html("");
        key = $("#searchContent").val();
        lock = false;
        page = 0;
        Load();
    });


    $(".deleteR").click(function () {
        var id = $(this).children("input").val();
        $.post("/Resource/Delete/" + id, function (data) {
            if (data == "ok") {
                alert("删除成功！");
                window.location.href = "/Home/Index";
            } else {
                alert("删除失败！");
                window.location.reload();
            }
        })
    });

    $("#btnSubReply").click(function () {
        var content = $("#reply_content").val();
        var rid = $("#resourceId").val();
        console.log(content);
        $.post("/Reply/Add", { "content": content, "rid": rid }).done(function (data) {
            if (data == "nouser") {
                alert("请先登录，在进行评论！");
            }
            else if (data == "ok") {
                alert("评论成功！");
                location.reload();
            }
            else {
                alert("评论失败！");
                location.reload();
            }
        })
    });
});
