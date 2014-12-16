<%@ Page Language="c#" %>
<%@ Import Namespace="Mvolo.DirectoryListing" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="System.IO" %>
<script runat="server">

bool ascending = true;
String sort;
String playMode;
	
void Page_Load()
{
    String sortBy = Request.QueryString["sortby"];
	String mode = Request.QueryString["playmode"];
	
	if (!String.IsNullOrEmpty(mode)){
		playMode = mode;
	} else {
		playMode = "Single";
	}
    //
    // Databind to the directory listing
    //
    DirectoryListingEntryCollection listing = 
        Context.Items[DirectoryListingModule.DirectoryListingContextKey] as DirectoryListingEntryCollection;
    
    if (listing == null)
    {
        throw new Exception("This page cannot be used without the DirectoryListing module");
    }

    //
    // Handle sorting
    //
    if (!String.IsNullOrEmpty(sortBy))
    {
		
        if (sortBy.Equals("name"))
        {
			sort = "name";
            listing.Sort(DirectoryListingEntry.CompareFileNames);
        }
        else if (sortBy.Equals("namerev"))
        {
			sort = "name";
			ascending = false;
            listing.Sort(DirectoryListingEntry.CompareFileNamesReverse);
        }            
        else if (sortBy.Equals("date"))
        {
			sort = "date";
            listing.Sort(DirectoryListingEntry.CompareDatesModified);        
        }
        else if (sortBy.Equals("daterev"))
        {
			sort = "date";
			ascending = false;
            listing.Sort(DirectoryListingEntry.CompareDatesModifiedReverse);        
        }
        else if (sortBy.Equals("size"))
        {
			sort = "size";
            listing.Sort(DirectoryListingEntry.CompareFileSizes);
        }
        else if (sortBy.Equals("sizerev"))
        {
			sort = "size";
			ascending = false;
            listing.Sort(DirectoryListingEntry.CompareFileSizesReverse);
        }
    }

    DirectoryListing.DataSource = listing;
    DirectoryListing.DataBind();
        
    //
    //  Prepare the file counter label
    //
    FileCount.Text = listing.Count + " items.";
}

String GetFileSizeString(FileSystemInfo info)
{
    if (info is FileInfo)
    {
		if (((FileInfo)info).Length > 111111) {
			return String.Format("{0}MB", ((int)(((FileInfo)info).Length * 10 / (double)1024 / (double)1024) / (double)10));
		} else {
			return String.Format("{0}KB", ((int)(((FileInfo)info).Length * 10 / (double)1024) / (double)10));
		}
    }
    else
    {
        return String.Empty;
    }
}

String GetDateModified(FileSystemInfo info)
{
	if (info is FileInfo)
	{
		return ((FileInfo)info).LastWriteTime.ToString();
	}
	else
	{
		return String.Empty;
	}
}

String GetBreadCrumbLink()
{
	if (Context.Request.Path == "/") {
		return "Server File Browser";
	}
	
	String[] paths = Context.Request.Path.TrimStart('/').TrimEnd('/').Split('/');
	String newPath = "/";
	String output = "<a href=\"/\">Home</a> > ";
	
	
	foreach (String path in paths)
	{
		newPath = newPath + path + "/";
		if (newPath == Context.Request.Path) {
			output = output + path;
		} else {
			output = output + "<a href=\"" + newPath + "\">" + path + "</a> > ";
		}
	}
	
	return output;
}

String GetRev()
{
	if (ascending) {
		return "rev";
	} else {
		return "";
	}
}

String GetQueryString(String sort, String playMode)
{
	if (sort == null) {
		sort = "";
	}
	if (playMode == null) {
		playMode = "";
	}
	return "?sortby=" + sort + GetRev() + "&playMode=" + playMode;
}

String GetDirection(String column)
{
	if (column == sort) {
		if (ascending) {
			return @"/\";
		} else {
			return @"\/";
		}
	} else {
		return "";
	}
}
</script>
<html xmlns="http://www.w3.org/1999/xhtml">
    <head>
        <title>Directory contents of <%= Context.Request.Path %></title>
        <style type="text/css">
            a { text-decoration: none; }
            a:hover { text-decoration: underline; }
            p {font-family: verdana; font-size: 10pt; }
            h2 {font-family: verdana; }
            td {font-family: verdana; font-size: 10pt; height:35px; }   
			tr {border-top:solid 1px #CCCCCC;}
			#DirectoryListing { width:100%; border-bottom:solid 1px #CCCCCC; }
			.FileLink {display:block; width:100%; height:35px; line-height:35px;}
			table { table-layout:fixed; }
        </style>
		<script type="text/javascript">
			function date_time(id){
				date = new Date;
				year = date.getFullYear();
				month = date.getMonth();
				months = new Array('January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December');
				d = date.getDate();
				day = date.getDay();
				days = new Array('Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday');
				h = date.getHours();
				if(h<10)
				{
						h = "0"+h;
				}
				m = date.getMinutes();
				if(m<10)
				{
						m = "0"+m;
				}
				s = date.getSeconds();
				if(s<10)
				{
						s = "0"+s;
				}
				result = ''+days[day]+' '+months[month]+' '+d+' '+year+' '+h+':'+m+':'+s;
				document.getElementById(id).innerHTML = result;
				setTimeout('date_time("'+id+'");','1000');
				return true;
			}
		</script>
    </head>
    <body>
        <h2><%= GetBreadCrumbLink() %></h2>
		<div style="float:right;">
		<%# if (playMode == "Shuffle") { %>
			<p>Play: <a href="<%# GetQueryString(Request.QueryString["sortby"], "Single") %>">Shuffle</a></p>
		<%# } else if (playMode == "Continuous") { %>
			<p>Play: <a href="<%# GetQueryString(Request.QueryString["sortby"], "Shuffle") %>">Continuous</a></p>
		<%# } else { %>
			<p>Play: <a href="<%# GetQueryString(Request.QueryString["sortby"], "Continuous") %>">Single</a></p>
		<%# } %>
		</div>
        <form runat="server">

			<asp:DataList id="DirectoryListing" runat="server">
				<HeaderTemplate>
					</td>
					<td style="width:77%;"><a class="FileLink" href="<%# GetQueryString("name", Request.QueryString["playMode"]) %>">Name <%# GetDirection("name") %></a></td>
					<td style="width:11%;"><a class="FileLink" href="<%# GetQueryString("name", Request.QueryString["playMode"]) %>">Date modified <%# GetDirection("date") %></a></td>
					<td style="width:8%;"><a class="FileLink" href="<%# GetQueryString("name", Request.QueryString["playMode"]) %>">Size <%# GetDirection("size") %></a>
				</HeaderTemplate>
				<ItemTemplate>
							<img alt="icon" src="/geticon.axd?file=<%# Path.GetExtension(((DirectoryListingEntry)Container.DataItem).Path) %>" /></td>
						<td style="white-space:nowrap;overflow:hidden;"><a class="FileLink" href="<%# ((DirectoryListingEntry)Container.DataItem).VirtualPath  %>">
							<%# ((DirectoryListingEntry)Container.DataItem).Filename %></a></td>
						<td><%# GetDateModified(((DirectoryListingEntry)Container.DataItem).FileSystemInfo) %></td>
						<td><%# GetFileSizeString(((DirectoryListingEntry)Container.DataItem).FileSystemInfo) %>
				</ItemTemplate>
			</asp:DataList>
			
			<p id="DT" style="float:right;"></p>
			<script type="text/javascript">date_time('DT');</script>
				
            <p>
				<asp:Label runat="Server" id="FileCount" />
            </p>
        </form>
    </body>
</html>