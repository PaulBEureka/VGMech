﻿<%@ Page Title="" Language="C#" MasterPageFile="~/AdminSite.Master" AutoEventWireup="true" ValidateRequest="false" CodeBehind="AdminCardPage.aspx.cs" Inherits="VisualMech.WebForm6" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!-- Page Heading -->
    <div class="d-sm-flex align-items-center justify-content-between mb-4">
        <h1 class="h3 mb-0 text-gray-800">Card Management</h1>
    </div>

    <hr class="thicker-line-10"/>

    <asp:Button runat="server" ID="BackBtn" OnClick="BackBtn_Click" CausesValidation="false" Visible="false" CssClass="btn btn-primary bg-gradient h-100" Text="<< Back"/>

    <asp:Panel runat="server" ID="PreviewCardPanel">
        <div class="row">
            <div class="col">
                <div class="row">
                    <div class="col-3 d-grid gap-2">
                        <div class="row">
                            <asp:Button runat="server" CssClass="btn btn-success my-auto w-100 h-100" Text="Add Learn Card" ID="AddLearnBtn" OnClick="AddLearnBtn_Click"/>
                        </div>
                        <div class="row">
                            <asp:Button runat="server" CssClass="btn btn-success my-auto w-100 h-100" Width="450px" Text="Edit/Delete Learn" ID="EditLearnBtn" OnClick="EditLearnBtn_Click"/>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <ul>
                        <asp:Literal runat="server" ID="LearnCardsPreviewLit"></asp:Literal>
                    </ul>

                </div>
            </div>
            <div class="col">
                <div class="row">
                    <div class="col-3 d-grid gap-2">
                        <div class="row">
                            <asp:Button runat="server" CssClass="btn btn-success my-auto h-100" Text="Add Minigame Card" ID="AddMiniBtn" OnClick="AddMiniBtn_Click"/>
                        </div>
                        <div class="row">
                            <asp:Button runat="server" CssClass="btn btn-success my-auto h-100" Text="Edit/Delete Minigame" ID="EditMiniBtn" OnClick="EditMiniBtn_Click"/>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <ul>
                        <asp:Literal runat="server" ID="MinigameCardsPreviewLit"></asp:Literal>
                    </ul>
                
                </div>
            </div>
        </div>
    </asp:Panel>



    <asp:Panel runat="server" ID="AddLearnPanel" Visible ="false">
        <div class="row py-2">
            <h3 class="text-black">Add Learn Card</h3>
        </div>
        <div class="row text-center">
            <asp:Label ID="SuccessLbl" runat="server" Visible="false" Text="Adding of Learn Card is successful!" CssClass="fs-5 text-success fw-bold"></asp:Label>
            <asp:Label ID="ErrorLbl" runat="server" Visible="false" Text="Error occurred" CssClass="fs-5 text-warning fw-bold"></asp:Label>
        </div>
        <div class="row">
                <div class="col-md-6 mb-3">
                    <label for="TitleInput" class="form-label text-black">Game Mechanic Title</label>
                    <asp:TextBox ID="TitleInput" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="TitleInputValidator" runat="server" ControlToValidate="TitleInput" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                </div>

                <div class="col-md-6 mb-3">
                    <label for="BgInputFile" class="form-label text-black">Background Image</label>
                    <asp:FileUpload ID="BgInputFile" runat="server" CssClass="form-control" accept="image/*" />
                    <asp:RequiredFieldValidator ID="BgInputFileValidator" runat="server" ControlToValidate="BgInputFile" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                    <asp:Label ID="BgFileHelp" runat="server" CssClass="form-text" Text="This will serve as the card background." />
                </div>

                <div class="col-md-6 mb-3">
                    <label for="IconInputFile" class="form-label text-black">Thumbnail Icon</label>
                    <asp:FileUpload ID="IconInputFile" runat="server" CssClass="form-control" accept="image/*" />
                    <asp:RequiredFieldValidator ID="IconInputFileValidator" runat="server" ControlToValidate="IconInputFile" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                    <asp:Label ID="IconfileHelp" runat="server" CssClass="form-text" Text="This will serve as the card icon thumbnail." />
                </div>

                <div class="col-md-6 mb-3">
                    <label for="DecriptionInput" class="form-label text-black">Description</label>
                    <asp:TextBox ID="DecriptionInput" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="DescriptionInputValidator" runat="server" ControlToValidate="DecriptionInput" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                    <asp:Label ID="DescHelp" runat="server" CssClass="form-text" Text="Description of the learning game mechanic." />
                </div>

                <div class="col-md-6 mb-3">
                    <label for="UnityLinkInput" class="form-label text-black">Unity link</label>
                    <asp:TextBox ID="UnityLinkInput" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="UnityLinkInputValidator" runat="server" ControlToValidate="UnityLinkInput" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                    <asp:Label ID="UnityHelp" runat="server" CssClass="form-text" Text="Link of the Unity Interactive Mechanic." />
                </div>

                <div class="col-md-6 mb-3">
                    <label for="CommonGenreInput" class="form-label text-black">Common Genres</label>
                    <asp:TextBox ID="CommonGenreInput" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="CommonGenreInputValidator" runat="server" ControlToValidate="CommonGenreInput" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                    <asp:Label ID="GenreHelp" runat="server" CssClass="form-text" Text="Most commonly applied genres of the game mechanic." />
                </div>

                <div class="col-md-6 mb-3">
                    <label for="VariationInput" class="form-label text-black">Possible Variations</label>
                    <asp:TextBox ID="VariationInput" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="VariationInputValidator" runat="server" ControlToValidate="VariationInput" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                    <asp:Label ID="VariationHelp" runat="server" CssClass="form-text" Text="Variations of the game mechanic." />
                </div>

                <div class="col-md-6 mb-3">
                    <label for="CombiInput" class="form-label text-black">Possible Combinations</label>
                    <asp:TextBox ID="CombiInput" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="CombiInputValidator" runat="server" ControlToValidate="CombiInput" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                    <asp:Label ID="CombiHelp" runat="server" CssClass="form-text" Text="Game mechanics that could be combined." />
                </div>

                <div class="col-md-6 mb-3">
                    <label for="ControlInput" class="form-label text-black">Interactive Controls</label>
                    <asp:TextBox ID="ControlInput" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="ControlInputValidator" runat="server" ControlToValidate="ControlInput" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                    <asp:Label ID="ControlHelp" runat="server" CssClass="form-text" Text="Input keys to be used for the Unity Interactive Mechanic." />
                </div>

                <div class="col-md-6 mb-3">
                    <label for="CodeSampleInput" class="form-label text-black">Sample Code Implementation</label>
                    <asp:TextBox ID="CodeSampleInput" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="5" style="height:150px;"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="CodeSampleInputValidator" runat="server" ControlToValidate="CodeSampleInput" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                    <asp:Label ID="CodeHelp" runat="server" CssClass="form-text" Text="Text of the sample code implementation here." />
                </div>

                <div class="col-md-12 mb-3">
                    <div class="form-check">
                        <asp:CheckBox ID="NotifyLearnCheckbox" runat="server" CssClass="" />
                        <label class="form-check-label text-black" for="exampleCheck1">Notify all users upon addition of game mechanic</label>
                    </div>
                </div>

                <div class="col-md-12">
                    <hr class="thicker-line-10"/>
                </div>

              <div class="col-md-12 mb-5">
                  <a class="btn btn-success h-100" href="#" data-bs-toggle="modal" data-bs-target="#createLearnModal">
                    <i class="fa-solid fa-pencil fa-sm fa-fw mr-2 text-gray-400"></i>
                      Create Learn Mechanic
                  </a>
              </div>




              
        </div>

        


        


        <!-- Logout Modal-->
            <div class="modal fade" id="createLearnModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel"
                aria-hidden="true">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" id="exampleModalLabel">Create Learn Mechanic?</h5>
                            <button class="close" type="button" data-bs-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">×</span>
                            </button>
                        </div>
                        <div class="modal-body">Select "Create" to proceed</div>
                        <div class="modal-footer">
                            <a class="btn btn-secondary" data-bs-dismiss="modal">Cancel</a>
                            <asp:Button runat="server" ID="CreateLearnBtn" OnClick="CreateLearnBtn_Click" CssClass="btn btn-success h-100" Text="Create" ></asp:Button>
                        </div>
                    </div>
                </div>
            </div>
    </asp:Panel>


    <asp:Panel runat="server" ID="AddMinigamePanel" Visible ="false">
        <div class="row py-2">
            <h3 class="text-black">Add Minigame Card</h3>
        </div>
        <div class="row text-center">
            <asp:Label ID="SuccessLbl2" runat="server" Visible="false" Text="Adding of Minigame Card is successful!" CssClass="fs-5 text-success fw-bold"></asp:Label>
            <asp:Label ID="ErrorLbl2" runat="server" Visible="false" Text="Error occurred" CssClass="fs-5 text-warning fw-bold"></asp:Label>
        </div>
        <div class="row">
                <div class="col-md-6 mb-3">
                    <label for="TitleInputMini" class="form-label text-black">Minigame Title</label>
                    <asp:TextBox ID="TitleInputMini" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="TitleValidatorMini" runat="server" ControlToValidate="TitleInputMini" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                </div>

                <div class="col-md-6 mb-3">
                    <label for="BgInputFileMini" class="form-label text-black">Background Image</label>
                    <asp:FileUpload ID="BgInputFileMini" runat="server" CssClass="form-control" accept="image/*" />
                    <asp:RequiredFieldValidator ID="BgInputFileValidatorMini" runat="server" ControlToValidate="BgInputFileMini" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                    <asp:Label ID="BgInputFileMiniLbl" runat="server" CssClass="form-text" Text="This will serve as the card background." />
                </div>

                <div class="col-md-6 mb-3">
                    <label for="UnityLinkInput" class="form-label text-black">Unity link</label>
                    <asp:TextBox ID="UnityLinkInputMini" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="UnityLinkInputMiniValidator" runat="server" ControlToValidate="UnityLinkInputMini" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                    <asp:Label ID="UnityLinkInputMiniLbl" runat="server" CssClass="form-text" Text="Link of the Unity Minigame." />
                </div>

                <div class="col-md-12 mb-3">
                    <div class="form-check">
                        <asp:CheckBox ID="NotifyMiniCheckbox" runat="server" CssClass="" />
                        <label class="form-check-label text-black" for="exampleCheck1">Notify all users upon addition of minigame</label>
                    </div>
                </div>
                
                <div class="col-md-12">
                    <hr class="thicker-line-10"/>
                </div>

              <div class="col-md-12 mb-5">
                  <a class="btn btn-success h-100" href="#" data-bs-toggle="modal" data-bs-target="#createMiniModal">
                    <i class="fa-solid fa-pencil fa-sm fa-fw mr-2 text-gray-400"></i>
                      Create Minigame
                  </a>
              </div>




              
        </div>

        


        


        <!-- Logout Modal-->
            <div class="modal fade" id="createMiniModal" tabindex="-1" role="dialog" aria-labelledby="miniModalLbl"
                aria-hidden="true">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" id="miniModalLbl">Create Minigame?</h5>
                            <button class="close" type="button" data-bs-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">×</span>
                            </button>
                        </div>
                        <div class="modal-body">Select "Create" to proceed</div>
                        <div class="modal-footer">
                            <a class="btn btn-secondary" data-bs-dismiss="modal">Cancel</a>
                            <asp:Button runat="server" ID="CreateMiniBtn" OnClick="CreateMiniBtn_Click" CssClass="btn btn-success h-100" Text="Create" ></asp:Button>
                        </div>
                    </div>
                </div>
            </div>
    </asp:Panel>

    <asp:Panel runat="server" ID="EditLearnPanel" Visible ="false">
        <div class="row py-2">
            <h3 class="text-black">Edit/Delete Learn Card</h3>
        </div>
        <div class="row overflow-auto d-grid">
            <asp:GridView runat="server" ID="LearnGridView" AutoGenerateEditButton="true" AutoGenerateDeleteButton="true" DataKeyNames="CardID"
                 OnRowCancelingEdit="LearnGridView_RowCancelingEdit" OnRowEditing="LearnGridView_RowEditing" AutoGenerateColumns="false"
                 OnRowUpdating="LearnGridView_RowUpdating" OnRowDataBound="LearnGridView_RowDataBound" CssClass="gridview-scrollable mx-auto"
                 OnRowDeleting="LearnGridView_RowDeleting" >
            <Columns>
                <asp:TemplateField HeaderText="Card ID">
                    <ItemTemplate>
                        <asp:Label ID="lblCardID" runat="server" Text='<%# Eval("CardID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Image Source">
                    <ItemTemplate>
                        <asp:Label ID="lblImageSource" runat="server" Text='<%# Eval("ImageSource") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtImageSource" runat="server" ValidateRequestMode="Disabled" Text='<%# Bind("ImageSource") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Thumbnail Source">
                    <ItemTemplate>
                        <asp:Label ID="lblThumbSource" runat="server" Text='<%# Eval("ThumbSource") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtThumbSource" runat="server" ValidateRequestMode="Disabled" Text='<%# Bind("ThumbSource") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Title">
                    <ItemTemplate>
                        <asp:Label ID="lblTitle" runat="server" Text='<%# Eval("Title") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtTitle" runat="server" ValidateRequestMode="Disabled" Text='<%# Bind("Title") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Description">
                    <ItemTemplate>
                        <asp:Label ID="lblDescription" runat="server" Text='<%# Eval("Description") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtDescription" runat="server" ValidateRequestMode="Disabled" Text='<%# Bind("Description") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Unity Link">
                    <ItemTemplate>
                        <asp:Label ID="lblUnityLink" runat="server" Text='<%# Eval("UnityLink") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtUnityLink" runat="server" ValidateRequestMode="Disabled" Text='<%# Bind("UnityLink") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Code Text">
                    <ItemTemplate>
                        <asp:Label ID="lblCodeText" runat="server" Text='<%# Eval("CodeText") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtCodeText" runat="server" ValidateRequestMode="Disabled" Text='<%# Bind("CodeText") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Common Genres">
                    <ItemTemplate>
                        <asp:Label ID="lblCommonGenres" runat="server" Text='<%# Eval("CommonGenres") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtCommonGenres" runat="server" ValidateRequestMode="Disabled" Text='<%# Bind("CommonGenres") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Possible Variations">
                    <ItemTemplate>
                        <asp:Label ID="lblPossibleVariations" runat="server" Text='<%# Eval("PossibleVariations") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtPossibleVariations" runat="server" ValidateRequestMode="Disabled" Text='<%# Bind("PossibleVariations") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Possible Combinations">
                    <ItemTemplate>
                        <asp:Label ID="lblPossibleCombinations" runat="server" Text='<%# Eval("PossibleCombinations") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtPossibleCombinations" runat="server" ValidateRequestMode="Disabled"  Text='<%# Bind("PossibleCombinations") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Interactive Controls">
                    <ItemTemplate>
                        <asp:Label ID="lblInteractiveControls" runat="server" Text='<%# Eval("InteractiveControls") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtInteractiveControls" runat="server" ValidateRequestMode="Disabled" Text='<%# Bind("InteractiveControls") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
        
            </Columns>
            
            
            
            
            </asp:GridView>


            
        </div>
    </asp:Panel>
    

    <asp:Panel runat="server" ID="EditMiniPanel" Visible ="false">
        <div class="row py-2">
            <h3 class="text-black">Edit/Delete Minigame Card</h3>
        </div>
        <div class="row overflow-auto d-grid">
            <asp:GridView runat="server" ID="MiniGridView" AutoGenerateEditButton="true" AutoGenerateDeleteButton="true" DataKeyNames="CardID"
                 OnRowCancelingEdit="MiniGridView_RowCancelingEdit" OnRowEditing="MiniGridView_RowEditing" AutoGenerateColumns="false"
                 OnRowUpdating="MiniGridView_RowUpdating" OnRowDataBound="MiniGridView_RowDataBound" CssClass="gridview-scrollable mx-auto"
                 OnRowDeleting="MiniGridView_RowDeleting" >
            <Columns>
                <asp:TemplateField HeaderText="Card ID">
                    <ItemTemplate>
                        <asp:Label ID="lblCardIDMini" runat="server" Text='<%# Eval("CardID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Thumbnail Source">
                    <ItemTemplate>
                        <asp:Label ID="lblThumbSourceMini" runat="server" Text='<%# Eval("ThumbSource") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtThumbSourceMini" runat="server" ValidateRequestMode="Disabled" Text='<%# Bind("ThumbSource") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Title">
                    <ItemTemplate>
                        <asp:Label ID="lblTitleMini" runat="server" Text='<%# Eval("Title") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtTitleMini" runat="server" ValidateRequestMode="Disabled" Text='<%# Bind("Title") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Unity Link">
                    <ItemTemplate>
                        <asp:Label ID="lblUnityLinkMini" runat="server" Text='<%# Eval("UnityLink") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtUnityLinkMini" runat="server" ValidateRequestMode="Disabled" Text='<%# Bind("UnityLink") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
        
            </Columns>
            
            
            
            
            </asp:GridView>


            
        </div>
    </asp:Panel>


    <script type="text/javascript">
        function confirmDelete(cardID) {
        return confirm("Are you sure you want to delete this record?");
    }
    </script>
</asp:Content>
