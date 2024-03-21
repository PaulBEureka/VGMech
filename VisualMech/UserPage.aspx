<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserPage.aspx.cs" Inherits="VisualMech.WebForm4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href='https://fonts.googleapis.com/css?family=Open+Sans:400,600,700' rel='stylesheet' type='text/css'>
    <webopt:bundlereference runat="server" path="~/Content/css" />
    <link rel="stylesheet" href="/Content/custom_styles.css">
    <script src="Scripts/jquery-3.4.1.js"></script>
    <script src="Scripts/jquery.signalR-2.4.3.js"></script>
    <script src="signalr/hubs"></script>
    
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.11.6/dist/umd/popper.min.js" integrity="sha384-oBqDVmMz9ATKxIep9tiCxS/Z9fNfEXiDAYTujMAeBAsjFuCZSmKbSSUnQlmh/jp3" crossorigin="anonymous"></script>

    <script src="/Content/custom.js"></script>



    <main>
        <div class="container">
            <div class="main-body">
              <div class="row gutters-sm">
                <div class="col-md-4 mb-3">
                  <div class="card_user">
                    <div class="card-body-user">
                      <div class="d-flex flex-column align-items-center text-center">
                        <img src="Images/person_icon.png" alt="User" class="rounded-circle" width="150">
                        <div class="mt-3">
                          <h4>Username here</h4>
                          
                        </div>
                      </div>
                    </div>
                  </div>
                  <div class="card_user mt-3">
                      <div class="card-body-user">
                        <h6 class="d-flex align-items-center mb-3">Learn Completion Status</h6>
                        <div class="progress">
                            <div class="progress-bar progress-bar-striped progress-bar-animated bg-danger" role="progressbar" aria-valuenow="75" aria-valuemin="0" aria-valuemax="100" style="width: 75%"></div>
                        </div>
                        <div class="text-center">
                            75% of all Learn Mechanics Completed
                        </div>
                      </div>
                  </div>
                </div>
                <div class="col-md-8">
                  <div class="card_user mb-3" id="userInfoDiv">
                    <div class="card-body-user">
                      <div class="row">
                        <div class="col-sm-3">
                          <h6 class="mb-0">Username</h6>
                        </div>
                        <div class="col-sm-9 text-secondary">
                          Username here
                        </div>
                      </div>
                      <hr>
                      <div class="row">
                        <div class="col-sm-3">
                          <h6 class="mb-0">Email</h6>
                        </div>
                        <div class="col-sm-9 text-secondary">
                          Email here
                        </div>
                      </div>
                      <hr>
                      <div class="row">
                        <div class="col-sm-3">
                          <h6 class="mb-0">About Me</h6>
                        </div>
                        <div class="col-sm-9 text-secondary">
                          About Me here
                        </div>
                      </div>
                      <hr>
                      <div class="row">
                        <div class="col-sm-12">
                          <button type="button" id="" class="comment_button my-2 bg-danger" onclick="changeContent()">Edit</button>
                          <button type="button" id="" class="comment_button my-2 bg-danger" onclick="changeContent()">Change Password</button>
                                     
                        
                        </div>
                      </div>
                    </div>
                  </div>

                  <div class="row gutters-sm">
                    <div class="col-sm-6 mb-3">
                      <div class="card_user h-100">
                        <div class="card-body-user">
                            <h6 class="d-flex align-items-center mb-3">Recommended New Learn Game Mechanics</h6>
                            <ul class="list-group list-group-flush">
                                <li class="list-group-item"><a href="#">First Mechanic Title Here</a></li>
                                <li class="list-group-item"><a href="#">Second Mechanic Title Here</a></li>
                                <li class="list-group-item"><a href="#">Third Mechanic Title Here</a></li>
                                <li class="list-group-item"><a href="#">Fourth Mechanic Title Here</a></li>
                                <li class="list-group-item"><a href="#">Fifth Mechanic Title Here</a></li>
                            </ul>
                        </div>
                      </div>
                    </div>
                    <div class="col-sm-6 mb-3">
                      <div class="card_user h-100">
                        <div class="card-body-user">
                            <h6 class="d-flex align-items-center mb-3">Visited Learn Game Mechanics</h6>
                            <ul class="list-group list-group-flush">
                                <li class="list-group-item">Date visited: 12/4/24 <br /><a href="#">First Mechanic Title Here</a></li>
                                <li class="list-group-item">Date visited: 12/4/24 <br /><a href="#">Second Mechanic Title Here</a></li>
                                <li class="list-group-item">Date visited: 12/4/24 <br /><a href="#">Third Mechanic Title Here</a></li>
                                <li class="list-group-item">Date visited: 12/4/24 <br /><a href="#">Fourth Mechanic Title Here</a></li>
                                <li class="list-group-item">Date visited: 12/4/24 <br /><a href="#">Fifth Mechanic Title Here</a></li>
                            </ul>
                        </div>
                      </div>
                    </div>
                  </div>

                  <div class="row gutters-sm">
                    
                    <div class="col-sm-6 mb-3">
                      <div class="card_user h-100">
                        <div class="card-body-user">
                            <h6 class="d-flex align-items-center mb-3">Friends</h6>
                            
                        </div>
                      </div>
                    </div>
                  </div>

                </div>
              </div>

            </div>
        </div>

    </main>
    
    <script>
        function changeContent() {
            var firstComment = commentHTML[0];

            $('#userInfoDiv').html(commentHTML[2]);
        }
    </script>
    

    <script src="https://kit.fontawesome.com/d7d0e3dd38.js" crossorigin="anonymous"></script>

    


</asp:Content>
