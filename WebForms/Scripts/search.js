jQuery(function () {
    MessageBox.show("z")
        $("#TextBox1").focus();
        $("form").submit();

        $("#TextBox1").keyup(function () {
            $("form").submit();
        });
    }
)

/*
_Layout.cshtml
    @RenderSection("scripts", required: false)
	</body>
	</html>


Ajax.cshtml
	@section scripts {
		<script src="~/Scripts/jquery.unobtrusive-ajax.js" type="text/javascript"></script>
		<script src="~/Scripts/Search.js" type="text/javascript"></script>
	}
*/