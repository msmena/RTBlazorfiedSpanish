﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;

/**
 * Author: Ryan A. Kueter
 * For the full copyright and license information, please view the LICENSE
 * file that was distributed with this source code.
 */
namespace RichTextBlazorfied;

public partial class RTBlazorfied
{
    [Inject]
    private IJSRuntime js { get; set; } = default!;

    [Parameter]
    public string? Value { get; set; }
    [Parameter]
    public EventCallback<string> ValueChanged { get; set; }
    private bool _settingParameter;

    [JSInvokable]
    public async Task UpdateValue(string value)
    {
        _settingParameter = true;
        if (value is not null)
        {
            Value = value;
            await ValueChanged.InvokeAsync(value);
        }
        _settingParameter = false;
    }
    [Parameter]
    public string? Width { get; set; }
    [Parameter]
    public string? Height { get; set; }

    private string GetStyles() =>
        $$"""
        .rich-text-box-tool-bar {
            background-color: {{ToolbarBackgroundColor}};
            border-bottom-style: {{ToolbarBorderStyle}};
            border-bottom-width: {{ToolbarBorderWidth}};
            border-bottom-color: {{ToolbarBorderColor}};
            border-bottom-radius: {{ToolbarBorderRadius}};
            padding-left: 3px;
            display: flex;
            flex-wrap: wrap;
            justify-content: flex-start;
        }
        .rich-text-box-tool-bar button {
            background-color: {{ButtonBackgroundColor}};
            border-style: {{ButtonBorderStyle}};
            border-width: {{ButtonBorderWidth}};
            border-color: {{ButtonBorderColor}};
            border-radius: {{ButtonBorderRadius}};
            color: {{ButtonTextColor}};
            outline: none;
            cursor: pointer;
            transition: 0.3s;
            min-height: calc({{ButtonTextSize}} + 14px);
            font-family: {{ButtonTextFont}};
            margin: 4px 1px;
        }
        .rich-text-box-tool-bar button:hover {
            background-color: {{ButtonBackgroundColorHover}};
            border-color: {{ButtonBorderColorHover}};
        }

        .rich-text-box-tool-bar button.selected {
            background-color: {{ButtonBackgroundColorSelected}};
            border-color: {{ButtonBorderColorSelected}};
        }

        .rich-text-box-tool-bar button:disabled {
            background-color: transparent;
            color: #999;
            cursor: default;
        }

        .rich-text-box-tool-bar button svg {
            fill: {{ButtonTextColor}};
            width: {{ButtonTextSize}};
            height: {{ButtonTextSize}};
        }

        .rich-text-box-tool-bar button:hover svg {
            fill: {{ButtonTextColor}};
        }

        .rich-text-box-tool-bar button:disabled svg {
            fill: #999;
        }

        .rich-text-box-menu-item {

        }

        .rich-text-box-menu-item-special {

        }

        .rich-text-box-menu-item svg, .rich-text-box-menu-item-special svg {
          display: block;
          height: auto;
          width: auto;
          max-height: 100%;
          max-width: 100%;
        }

        .rich-text-box-menu-item:disabled {
            color: #999;
        }

        .rich-text-box-container {
            resize: both;
            overflow: {{EditorResizeHandle}};
            border-style: {{EditorBorderStyle}};
            border-width: {{EditorBorderWidth}};
            border-color: {{EditorBorderColor}};
            border-radius: {{EditorBorderRadius}};
            box-shadow: {{EditorBoxShadow}};
            max-width: {{EditorWidth}};
            height: {{EditorHeight}};
            display: flex;
            flex-direction: column;
            z-index: 1;
        }
        .rich-text-box-content-container {
            width: 100%;
            height: 100%;
            overflow: auto;
            display: flex;
            flex-direction: row;
            background-color: {{ContentBackgroundColor}};
            box-shadow: {{ContentBoxShadow}};
            
        }
        .rich-text-box-content {
            color: {{ContentTextColor}} !important;
            font-size: {{ContentTextSize}} !important;
            font-family: {{ContentTextFont}} !important;
            padding: 5px 10px;
            width: 100%;
            min-height: 25px;
            white-space: pre-wrap; 
            word-wrap: break-word;
            outline: 0px solid transparent;
        }
        .rich-text-box-content img {
            cursor: pointer;
        }
        //.rich-text-box-content::selection {
        //    background-color: green;
        //    color: black;
        //}
        .rich-text-box-source {
            padding: 10px;
            width: 100%;
            min-height: 25px;
            color: {{ContentTextColor}} !important;
            font-size: {{ContentTextSize}} !important;
            white-space: pre-wrap; 
            background-color: {{ContentBackgroundColor}};
            box-shadow: {{ContentBoxShadow}};
            border-style: none;
            display: none;
            resize: none;
            margin: 0;
            line-height: 1.6;
            font-size: 16px;
            outline: 0px solid transparent;
        }
        .rich-text-box-divider-btn {
            background-color: inherit;
            align-items: center;
            justify-content: center;
            text-align: center;
            border: none !important;
            outline: none;
            cursor: pointer;
            padding: 3px 4px;
            transition: 0.3s;
            margin: 4px 1px;
        }
        .rich-text-box-divider-btn[disabled], .rich-text-box-divider-btn[disabled]:hover, .rich-text-box-divider-btn[disabled]:focus, .rich-text-box-divider-btn[disabled]:active {
            background: unset;
            color: unset;
            cursor: default;
        }
        .rich-text-box-divider {
            min-height: 25px;
            height: {{ButtonTextSize}};
            background-color: {{ButtonTextColor}};
            display: block;
            border-left: .5px solid rgba(255, 255, 255, 0.6);
            opacity: .5;
        }
        .rich-text-box-scroll::-webkit-scrollbar {
            height: {{ScrollWidth}};
            width: {{ScrollWidth}};
            opacity: {{ScrollOpacity}};
        }
        .rich-text-box-scroll::-webkit-scrollbar-track {
            background-color: {{ScrollBackgroundColor}};
        }
        .rich-text-box-scroll::-webkit-scrollbar-thumb {
            background: {{ScrollThumbBackgroundHover}};
            border-radius: {{ScrollThumbBorderRadius}};
        }
        .rich-text-box-scroll::-webkit-scrollbar-thumb:hover {
            background: {{ScrollThumbBackgroundHover}};
            cursor: default;
        }

        .rich-text-box-dropdown {
          position: relative;
          display: inline-block;
        }

        .rich-text-box-dropdown-content {
          display: none;
          position: absolute;
          background-color: {{ToolbarDropdownBackgroundColor}};
          border-style: {{ButtonBorderStyle}};
          border-width: {{ButtonBorderWidth}};
          border-color: {{ButtonBorderColor}};
          border-radius: 5px;
          max-height: 200px;
          overflow: auto;
          box-shadow: 0px 4px 8px 0px rgba(0,0,0,0.2);
          font-family: Arial, sans-serif !important;
          z-index: 2;
        }

        .rich-text-box-dropdown-btn {
            font-size: {{ButtonTextSize}};
            min-height: calc({{ButtonTextSize}} + 14px);
            padding: 0 10px;
        }

        .rich-text-box-format-button {
            
        }
        .rich-text-box-format-content {
            min-width: 185px;
        }

        .rich-text-box-font-button {
            
        }
        .rich-text-box-font-content {
          min-width: 180px;
        }

        .rich-text-box-size-button {
            
        }
        .rich-text-box-size-content {
          min-width: 80px;
        }

        .rich-text-box-dropdown-content a {
          color: {{ToolbarDropdownTextColor}};
          font-size: 18px;
          padding: 10px 14px;
          text-decoration: none;
          display: block;
        }

        .rich-text-box-dropdown a:hover, .rich-text-box-menu-item.active {
          background-color: {{ToolbarDropdownBackgroundColorHover}};
          color: {{ToolbarDropdownTextColorHover}};
         }

        .rich-text-box-show {display: block;}

        .rich-text-box-modal {
          background-color: {{ModalBackgroundColor}};
          color: {{ModalTextColor}};
          font-size: {{ModalTextSize}};
          font-family: {{ModalTextFont}};
          margin: auto;
          border: 1px solid #888;
          width: 800px;
          box-shadow: 0 4px 8px 0 rgba(0,0,0,0.2),0 6px 20px 0 rgba(0,0,0,0.19);
          border-radius: 5px;
          user-select: none;
          z-index: 2;
        }
        .rich-text-box-modal-title {
            font-weight: bold; 
            font-size: calc({{ModalTextSize}} + 2);
        }
        .rich-text-box-form-left {
            float: left; 
            width: 380px;
        }
        .rich-text-box-form-right {
            float: right; 
            width: 380px;
        }
        @media screen and (max-width: 850px) {
            .rich-text-box-form-left {
                float: none; 
            }
            .rich-text-box-form-right {
                float: none; 
            }
            .rich-text-box-modal {
                width: 400px;
            }
        }
        @media screen and (max-width: 500px) {
            .rich-text-box-modal {
                width: 100%;
            }
        }
        .clearfix {
          overflow: auto;
        }

        .clearfix::after {
          content: "";
          clear: both;
          display: table;
        }
        
        .rich-text-box-modal-close {
          color: {{ModalTextColor}};
          font-size: 24px;
          cursor: pointer;
        }

        .rich-text-box-modal-close:hover,
        .rich-text-box-modal-close:focus {
          color: {{ModalTextColor}};
          text-decoration: none;
          cursor: pointer;
        }

        .rich-text-box-modal-body {
          padding: 2px 8px;
        }

        .rich-text-box-form-element {
          width: 100%;
          padding: 10px;
          font-size: 14px;
          background-color: {{ModalTextboxBackgroundColor}};
          color: {{ModalTextboxTextColor}};
          font-size: {{ModalTextSize}};
          font-family: {{ModalTextFont}};
          border-width: 1px;
          border-style: solid;
          border-color: {{ModalTextboxBorderColor}};
          outline: 0;
          border-radius: 0px;
          box-sizing: border-box;
          margin-top: 0px;
          margin-bottom: 16px;
          resize: vertical;
        }

        .rich-text-box-form-element:disabled {
            color: #999;
            border-color: #DDD;
            cursor: default;
        }

        .rich-text-box-form-checkbox {
          outline: none;
          width: 20px;
          height: 20px;
          margin-right: 8px;
          accent-color: {{ModalCheckboxAccentColor}}; 
        }
        .rich-text-box-form-checkbox:focus {
          border-style: solid;
          border-color: {{ModalTextboxBorderColor}};
          border-width: 1px;
          box-shadow: 0 0 5px 2px rgba(169, 169, 169, 0.8);          
        }
        
        .rich-text-box-upload-btn {
          padding: 10px 20px !important;
          font-size: {{ModalTextSize}};
          font-family: {{ModalTextFont}};
          transition: 0.3s;
          background-color: {{ButtonBackgroundColor}};
          border-style: {{ButtonBorderStyle}};
          border-width: {{ButtonBorderWidth}};
          border-color: {{ButtonBorderColor}};
          border-radius: {{ButtonBorderRadius}};
          color: {{ButtonTextColor}};
          outline: none;
          cursor: pointer;
          transition: 0.3s;
          min-height: calc({{ButtonTextSize}} + 14px);
          font-family: {{ButtonTextFont}};
          margin: 4px 1px;
        }

        .rich-text-box-upload-btn:hover {
            background-color: {{ButtonBackgroundColorHover}};
            border-color: {{ButtonBorderColorHover}};
        }

        .rich-text-box-form-button {
          padding: 10px 20px !important;
          font-size: {{ModalTextSize}};
          font-family: {{ModalTextFont}};
          transition: 0.3s;
        }

        .rich-text-box-form-button:focus {
          background-color: {{ButtonBackgroundColorHover}};
          border-color: {{ButtonBorderColorHover}};
        }

        .blazing-rich-text-color-picker-container {
            position: relative;
        }

        .blazing-rich-text-color-picker-button {
            min-height: calc({{ButtonTextSize}} + 14px);
        }

        .blazing-rich-text-color-picker-dropdown {
            width: 80px;
            padding: 10px 10px 6px 10px;
        }

        .blazing-rich-text-color-option {
            width: 15px;
            height: 15px;
            margin: 2px;
            cursor: pointer;
            display: inline-block;
            border: 1px solid #999;
        }

        .blazing-rich-text-color-option:hover {
            border-color: #000;
        }
        .blazing-rich-text-color-selection {
            width: 100%;
            border-style: solid;
            border-width: 1px;
            border-color: #999;
            height: 40px;
            cursor: pointer;
            display: inline-block;
        }
        .rich-text-box-quote {
          font-family: {{ModalTextFont}};
        }
        .rich-text-box-code {
          overflow: auto !important;
          white-space: pre;
        }

        /* Message Bar */
        .rich-text-box-message-bar {
            font-size: {{ContentTextSize}};
            background-color: rgba(0, 0, 0, 0.6);
            color: white;
            display: flex;
            justify-content: space-between;
            align-items: center;
            padding: 5px 10px;
            opacity: 1;
            transform: translateY(0);
            transition: opacity 0.2s ease-in-out, transform 0.2s ease-in-out;
            pointer-events: auto;
        }
        
        .rich-text-box-message-bar.rich-text-box-message-hidden {
            opacity: 0;
            height: 0;
            padding: 0;
            color: transparent;
            pointer-events: none;
        }
        
        .rich-text-box-message {
        }
        
        .rich-text-box-message-close-button {
            background: none;
            border: none;
            color: white;
            font-size: 18px;
            cursor: pointer;
        }
        /* End Message Bar */

        /* Color Picker */
        .rich-text-box-color-picker {
            width: 100%;
        }
        .rich-text-box-color-display {
            width: 100%;
            height: 50px;
            border: 1px solid #ccc;
            margin: 20px 0;
        }
        .rich-text-box-slider-container {
            display: flex;
            align-items: center;
            margin-bottom: 10px;
        }
        .rich-text-box-slider-container label {
            width: 20px;
            margin-right: 10px;
            color: {{ModalTextColor}};
        }
        .rich-text-box-range {
            width: 100%;
            -webkit-appearance: none;
        }
        .rich-text-box-range:focus {
            outline: none;
        }
        .rich-text-box-range::-webkit-slider-runnable-track {
             background: {{ModalTextColor}};
             height: 5px;
        }
        .rich-text-box-red-slider::-webkit-slider-runnable-track {
            background: linear-gradient(to right, rgb(0,0,0) 0%, rgb(255,0,0) 100%) !important;
        }
        .rich-text-box-green-slider::-webkit-slider-runnable-track {
            background: linear-gradient(to right, rgb(0,0,0) 0%, rgb(0,255,0) 100%) !important;
        }
        .rich-text-box-blue-slider::-webkit-slider-runnable-track {
            background: linear-gradient(to right, rgb(0,0,0) 0%, rgb(0,0,255) 100%) !important;
        }
        .rich-text-box-range::-moz-range-track {
             background: {{ModalTextColor}};
             height: 5px;
        }
        .rich-text-box-range::-webkit-slider-thumb {
             -webkit-appearance: none;
             height: 15px;
             width: 15px;
             background: {{ModalBackgroundColor}};
             margin-top: -5px;
             border-style: solid;
             border-width: 3px;
             border-color: {{ModalTextColor}};
             border-radius: 50%;
        }
        .rich-text-box-range::-moz-range-thumb {
             -webkit-appearance: none;
             height: 15px;
             width: 15px;
             background: {{ModalBackgroundColor}};
             margin-top: -5px;
             border-style: solid;
             border-width: 3px;
             border-color: {{ModalTextColor}};
             border-radius: 50%;
        }
        .rich-text-box-number {
            width: 50px;
            margin-left: 10px;
            width: 100px;
            padding: 5px;
            font-size: 14px;
            background-color: {{ModalTextboxBackgroundColor}};
            color: {{ModalTextboxTextColor}};
            border-width: 1px;
            border-style: solid;
            border-color: {{ModalTextboxBorderColor}};
            outline: 0;
            border-radius: 0px;
            box-sizing: border-box;
        }
        .rich-text-box-hex-container {
            display: flex;
            align-items: center;
            margin-top: 20px;
        }
        .rich-text-box-hex-input {
            width: 100px !important;
            margin-left: 10px;
        }

        /* Editor Styles */
        blockquote {
          background: #f9f9f9;
          border-left: 5px solid #ccc;
          margin: 1.5em 10px;
          padding: 0.5em 10px;
        }
        pre {
          background: #f9f9f9;
          border-radius: 10px;
          overflow-x: auto;
          white-space: pre-wrap;
          margin: 1.5em 10px;
          padding: 0.5em 10px;
        }
        table {
          border-collapse: collapse;
        }

        td, th {
          border: 1px solid #ccc;
          padding: 4px 6px;
          height: 25px;
          min-width: 100px;
        }
        """;

    #region Styles
    // Toolbar
    public string? ToolbarBackgroundColor { get; set; } = "#FFF";
    public string? ToolbarBorderStyle { get; set; } = "solid";
    public string? ToolbarBorderWidth { get; set; } = "1px";
    public string? ToolbarBorderColor { get; set; } = "#EEE";
    public string? ToolbarBorderRadius { get; set; } = "0px";
    public string? ToolbarDropdownBackgroundColor { get; set; } = "#FFF";
    public string? ToolbarDropdownTextColor { get; set; } = "#000";
    public string? ToolbarDropdownBackgroundColorHover { get; set; } = "#e5e5e5";
    public string? ToolbarDropdownTextColorHover { get; set; } = "#000";

    // Buttons
    public string? ButtonTextColor { get; set; } = "#000";
    public string? ButtonTextSize { get; set; } = "16px";
    public string? ButtonTextFont { get; set; } = "Arial, sans-serif";
    public string? ButtonBackgroundColor { get; set; } = "inherit";
    public string? ButtonBackgroundColorHover { get; set; } = "#DDD";
    public string? ButtonBackgroundColorSelected { get; set; } = "#CCC";
    public string? ButtonBorderRadius { get; set; } = "5px";
    public string? ButtonBorderStyle { get; set; } = "none";
    public string? ButtonBorderWidth { get; set; } = "0px";
    public string? ButtonBorderColor { get; set; } = "#AAA";
    public string? ButtonBorderColorHover { get; set; } = "inherit";
    public string? ButtonBorderColorSelected { get; set; } = "inherit";
    // Content
    public string? ContentTextColor { get; set; } = "#000";
    public string? ContentTextSize { get; set; } = "16px";
    public string? ContentTextFont { get; set; } = "Arial, sans-serif";
    public string? ContentBackgroundColor { get; set; } = "#FFF";
    public string? ContentBoxShadow { get; set; } = "none";

    // Editor
    public string? EditorWidth { get; set; } = "100%";
    public string? EditorHeight { get; set; } = "300px";
    public string? EditorBorderRadius { get; set; } = "0px";
    public string? EditorBorderStyle { get; set; } = "solid";
    public string? EditorBorderWidth { get; set; } = "1px";
    public string? EditorBorderColor { get; set; } = "#EEE";
    public string? EditorBoxShadow { get; set; } = "none";
    public string? EditorResizeHandle { get; set; } = "auto";

    // Scroll
    public string? ScrollWidth { get; set; } = "10px";
    public string? ScrollOpacity { get; set; } = "1";
    public string? ScrollBackgroundColor { get; set; } = "transparent";
    public string? ScrollThumbBackgroundHover { get; set; } = "#DDD";
    public string? ScrollThumbBackground { get; set; } = "#AAA";
    public string? ScrollThumbBorderRadius { get; set; } = "0";

    // Modal
    public bool MmodalRemoveCSSClassInputs { get; set; }
    public string? ModalBackgroundColor { get; set; } = "#fefefe";
    public string? ModalTextColor { get; set; } = "#000";
    public string? ModalTextSize { get; set; } = "16px";
    public string? ModalTextFont { get; set; } = "Arial, sans-serif";
    public string? ModalTextboxBackgroundColor { get; set; } = "#fff";
    public string? ModalTextboxTextColor { get; set; } = "#000";
    public string? ModalCheckboxAccentColor { get; set; } = "#007bff";
    public string? ModalTextboxBorderColor { get; set; } = "#CCC";
    #endregion

    public async Task<string?> GetPlainTextAsync() =>
        await js.InvokeAsync<string>("RTBlazorfied_Method", "plaintext", id);

    public async Task<string?> GetHtmlAsync() =>
        await js.InvokeAsync<string>("RTBlazorfied_Method", "html", id);

    private bool Editable { get; set; } = true;
    protected override void OnInitialized()
    {
        // Invoke the Options
        RTBlazorfiedGlobal.Instances.Add(id, this);
        if (Options is not null)
        {
            Options(_options!);
        }
        LoadOptions();
    }

    [Parameter]
    public Action<IRTBlazorfiedOptions>? Options { get; set; }
    private RichTextboxOptions _options { get; set; } = new();
    
    #region Options
    private void LoadOptions()
    {
        GetToolbarOptions();
        GetButtonOptions();
        GetEditorOptions();
        GetContentOptions();
        GetScrollOptions();
        GetModalOptions();
        GetButtons();
    }

    #region ButtonVisibility
    private bool? _font;
    private bool? _size;
    private bool? _format;
    private bool? _textstylesdivider;
    private bool? _bold;
    private bool? _italic;
    private bool? _underline;
    private bool? _strikethrough;
    private bool? _subscript;
    private bool? _superscript;
    private bool? _formatdivider;
    private bool? _textcolor;
    private bool? _textcolordivider;
    private bool? _alignleft;
    private bool? _aligncenter;
    private bool? _alignright;
    private bool? _alignjustify;
    private bool? _indent;
    private bool? _aligndivider;
    private bool? _copy;
    private bool? _cut;
    private bool? _paste;
    private bool? _delete;
    private bool? _selectall;
    private bool? _actiondivider;
    private bool? _undo;
    private bool? _redo;
    private bool? _historydivider;
    private bool? _link;
    private bool? _quote;
    private bool? _codeBlock;
    private bool? _table;
    private bool? _embedMedia;
    private bool? _image;
    private bool? _imageUpload;
    private bool? _mediadivider;
    private bool? _orderedlist;
    private bool? _unorderedlist;
    private bool? _listdivider;
    private bool? _htmlView;
    private bool? _preview;
    #endregion
    private void GetButtons()
    {
        var buttons = _options.GetButtonVisibilityOptions();
        if (buttons is null)
        {
            SetButtonDefaults();
        }
        else
        {
            if (buttons._clearAll is true)
            {
                SetButtonDefaults(false);
            }
            else
            {
                SetButtonDefaults();
            }

            GetToggleButton(buttons);
            GetTextStyleButtons(buttons);
            GetTextFormatButtons(buttons);
            GetTextColorButtons(buttons);
            GetAlignButtons(buttons);
            GetActionsButtons(buttons);
            GetUndoRedoButtons(buttons);
            GetInsertButtons(buttons);
            GetListButtons(buttons);
        }
    }

    private void GetToggleButton(RichTextboxButtonVisibilityOptions? buttons)
    {
        if (buttons is not null)
        {
            if (buttons.HtmlView is not null)
            {
                _htmlView = buttons.HtmlView;
            }
            if (buttons.Preview is not null)
            {
                _preview = buttons.Preview;
            }
        }
    }

    private void GetListButtons(RichTextboxButtonVisibilityOptions? buttons)
    {
        if (buttons is not null)
        {
            if (buttons.OrderedList is not null)
            {
                _orderedlist = buttons.OrderedList;
            }
            if (buttons.UnorderedList is not null)
            {
                _unorderedlist = buttons.UnorderedList;
            }
            if (buttons.Indent is not null)
            {
                _indent = buttons.Indent;
            }
            // If the user did not specify false, keep the button
            if (buttons.OrderedList == true
                || buttons.UnorderedList == true
                || buttons.Indent == true)
            {
                if (buttons.ListDivider is not null)
                {
                    _listdivider = buttons.ListDivider;
                }
                else
                {
                    _listdivider = true;
                }
            }
        }
    }

    private void GetInsertButtons(RichTextboxButtonVisibilityOptions? buttons)
    {
        if (buttons is not null)
        {
            if (buttons.Quote is not null)
            {
                _quote = buttons.Quote;
            }
            if (buttons.CodeBlock is not null)
            {
                _codeBlock = buttons.CodeBlock;
            }
            if (buttons.EmbedMedia is not null)
            {
                _embedMedia = buttons.EmbedMedia;
            }
            if (buttons.InsertTable is not null)
            {
                _table = buttons.InsertTable;
            }
            if (buttons.Link is not null)
            {
                _link = buttons.Link;
            }
            if (buttons.Image is not null)
            {
                _image = buttons.Image;
            }
            if (buttons.ImageUpload is not null)
            {
                _imageUpload = buttons.ImageUpload;
            }
            // If the user did not specify false, keep the button
            if (buttons.Link == true
                || buttons.Image == true
                || buttons.Quote == true
                || buttons.CodeBlock == true
                || buttons.EmbedMedia == true
                || buttons.InsertTable == true)
            {
                if (buttons.MediaDivider is not null)
                {
                    _mediadivider = buttons.MediaDivider;
                }
                else
                {
                    _mediadivider = true;
                }
            }
        }
    }

    private void GetUndoRedoButtons(RichTextboxButtonVisibilityOptions? buttons)
    {
        if (buttons is not null)
        {
            if (buttons.Undo is not null)
            {
                _undo = buttons.Undo;
            }
            if (buttons.Redo is not null)
            {
                _redo = buttons.Redo;
            }

            // If the user did not specify false, keep the button
            if (buttons.Undo == true
                || buttons.Redo == true)
            {
                if (buttons.HistoryDivider is not null)
                {
                    _historydivider = buttons.HistoryDivider;
                }
                else
                {
                    _historydivider = true;
                }
            }
        }
    }

    private void GetActionsButtons(RichTextboxButtonVisibilityOptions? buttons)
    {
        if (buttons is not null)
        {
            if (buttons.Copy is not null)
            {
                _copy = buttons.Copy;
            }
            if (buttons.Cut is not null)
            {
                _cut = buttons.Cut;
            }
            if (buttons.Paste is not null)
            {
                _paste = buttons.Paste;
            }
            if (buttons.Delete is not null)
            {
                _delete = buttons.Delete;
            }
            if (buttons.SelectAll is not null)
            {
                _selectall = buttons.SelectAll;
            }

            // If the user did not specify false, keep the button
            if (buttons.Copy == true
                || buttons.Cut == true
                || buttons.Paste == true
                || buttons.Delete == true
                || buttons.SelectAll == true)
            {
                if (buttons.ActionDivider is not null)
                {
                    _actiondivider = buttons.ActionDivider;
                }
                else
                {
                    _actiondivider = true;
                }
            }
        }
    }

    private void GetAlignButtons(RichTextboxButtonVisibilityOptions? buttons)
    {
        if (buttons is not null)
        {
            if (buttons.AlignLeft is not null)
            {
                _alignleft = buttons.AlignLeft;
            }
            if (buttons.AlignCenter is not null)
            {
                _aligncenter = buttons.AlignCenter;
            }
            if (buttons.AlignRight is not null)
            {
                _alignright = buttons.AlignRight;
            }
            if (buttons.AlignJustify is not null)
            {
                _alignjustify = buttons.AlignJustify;
            }

            // If the user did not specify false, keep the button
            if (buttons.AlignLeft == true
                || buttons.AlignCenter == true
                || buttons.AlignRight == true
                || buttons.AlignJustify == true)
            {
                if (buttons.AlignDivider is not null)
                {
                    _aligndivider = buttons.AlignDivider;
                }
                else
                {
                    _aligndivider = true;
                }
            }
        }
    }

    private void GetTextColorButtons(RichTextboxButtonVisibilityOptions? buttons)
    {
        if (buttons is not null)
        {
            if (buttons.TextColor is not null)
            {
                _textcolor = buttons.TextColor;
            }

            if (buttons.TextColor == true)
            {
                if (buttons.TextColorDivider is not null)
                {
                    _textcolordivider = buttons.TextColorDivider;
                }
                else
                {
                    _textcolordivider = true;
                }
            }
        }
    }

    private void GetTextFormatButtons(RichTextboxButtonVisibilityOptions? buttons)
    {
        if (buttons is not null)
        {
            if (buttons.Bold is not null)
            {
                _bold = buttons.Bold;
            }
            if (buttons.Italic is not null)
            {
                _italic = buttons.Italic;
            }
            if (buttons.Underline is not null)
            {
                _underline = buttons.Underline;
            }
            if (buttons.Strikethrough is not null)
            {
                _strikethrough = buttons.Strikethrough;
            }
            if (buttons.Subscript is not null)
            {
                _subscript = buttons.Subscript;
            }
            if (buttons.Superscript is not null)
            {
                _superscript = buttons.Superscript;
            }

            // If the user did not specify false, keep the button
            if (buttons.Bold == true
                || buttons.Italic == true
                || buttons.Underline == true
                || buttons.Strikethrough == true
                || buttons.Subscript == true
                || buttons.Superscript == true)
            {
                if (buttons.FormatDivider is not null)
                {
                    _formatdivider = buttons.FormatDivider;
                }
                else
                {
                    _formatdivider = true;
                }
            }
        }
    }

    private void GetTextStyleButtons(RichTextboxButtonVisibilityOptions buttons)
    {
        if (buttons.Font is not null)
        {
            _font = buttons.Font;
        }
        if (buttons.Size is not null)
        {
            _size = buttons.Size;
        }
        if (buttons.Format is not null)
        {
            _format = buttons.Format;
        }
        

        if (buttons.Font == true
            || buttons.Size == true
            || buttons.Format == true)
        {
            if (buttons.TextStylesDivider is not null)
            {
                _textstylesdivider = buttons.TextStylesDivider;
            }
            else
            {
                _textstylesdivider = true;
            }
        }
    }

    private void SetButtonDefaults(bool setting = true)
    {
        _font = setting;
        _size = setting;
        _format = setting;
        _bold = setting;
        _italic = setting;
        _underline = setting;
        _strikethrough = setting;
        _subscript = setting;
        _superscript = setting;
        _textcolor = setting;
        _alignleft = setting;
        _aligncenter = setting;
        _alignright = setting;
        _alignjustify = setting;
        _copy = setting;
        _cut = setting;
        _paste = setting;
        _delete = setting;
        _selectall = setting;
        _undo = setting;
        _redo = setting;
        _link = setting;
        _image = setting;
        _imageUpload = setting;
        _orderedlist = setting;
        _unorderedlist = setting;
        _indent = setting;
        _quote = setting;
        _codeBlock = setting;
        _embedMedia = setting;
        _table = setting;
        _htmlView = setting;
        _preview = setting;

        // Dividers
        _textstylesdivider = setting;
        _formatdivider = setting;
        _textcolordivider = setting;
        _aligndivider = setting;
        _actiondivider = setting;
        _historydivider = setting;
        _mediadivider = setting;
        _listdivider = setting;
    }

    private void GetScrollOptions()
    {
        var scrollOptions = _options.GetScrollOptions();
        if (scrollOptions is not null)
        {
            if (scrollOptions.Width is not null)
            {
                ScrollWidth = scrollOptions.Width;
            }
            if (scrollOptions.Opacity is not null)
            {
                ScrollOpacity = scrollOptions.Opacity;
            }
            if (scrollOptions.BackgroundColor is not null)
            {
                ScrollBackgroundColor = scrollOptions.BackgroundColor;
            }
            if (scrollOptions.ThumbBackgroundHover is not null)
            {
                ScrollThumbBackgroundHover = scrollOptions.ThumbBackgroundHover;
            }
            if (scrollOptions.ThumbBackground is not null)
            {
                ScrollThumbBackground = scrollOptions.ThumbBackground;
            }
            if (scrollOptions.ThumbBorderRadius is not null)
            {
                ScrollThumbBorderRadius = scrollOptions.ThumbBorderRadius;
            }
        }
    }

    private void GetContentOptions()
    {
        var styleOptions = _options.GetContentOptions();
        if (styleOptions is not null)
        {
            if (styleOptions.BackgroundColor is not null)
            {
                ContentBackgroundColor = styleOptions.BackgroundColor;
            }
            if (styleOptions.TextColor is not null)
            {
                ContentTextColor = styleOptions.TextColor;
            }
            if (styleOptions.TextSize is not null)
            {
                ContentTextSize = styleOptions.TextSize;
            }
            if (styleOptions.TextFont is not null)
            {
                ContentTextFont = styleOptions.TextFont;
            }
            if (styleOptions.ContentBoxShadow is not null)
            {
                ContentBoxShadow = styleOptions.ContentBoxShadow;
            }
        }
    }

    private void GetEditorOptions()
    {
        var styleOptions = _options.GetEditorOptions();
        if (styleOptions is not null)
        {
            if (styleOptions.Width is not null)
            {
                EditorWidth = styleOptions.Width;
            }
            if (styleOptions.Height is not null)
            {
                EditorHeight = styleOptions.Height;
            }
            if (styleOptions.BorderRadius is not null)
            {
                EditorBorderRadius = styleOptions.BorderRadius;
            }
            if (styleOptions.BorderStyle is not null)
            {
                EditorBorderStyle = styleOptions.BorderStyle;
            }
            if (styleOptions.BorderWidth is not null)
            {
                EditorBorderWidth = styleOptions.BorderWidth;
            }
            if (styleOptions.BorderColor is not null)
            {
                EditorBorderColor = styleOptions.BorderColor;
            }
            if (styleOptions.BoxShadow is not null)
            {
                EditorBoxShadow = styleOptions.BoxShadow;
            }
            if (styleOptions._removeResizeHandle is not null)
            {
                if (styleOptions._removeResizeHandle == true)
                {
                    EditorResizeHandle = "visible";
                }
                else
                {
                    EditorResizeHandle = "auto";
                }
            }
        }
        // Allow the inline styles to override the options
        if (Width is not null)
        {
            EditorWidth = Width;
        }
        if (Height is not null)
        {
            EditorHeight = Height;
        }
    }

    private void GetButtonOptions()
    {
        var buttonOptions = _options.GetButtonOptions();
        if (buttonOptions is not null)
        {
            if (buttonOptions.TextColor is not null)
            {
                ButtonTextColor = buttonOptions.TextColor;
            }
            if (buttonOptions.TextSize is not null)
            {
                ButtonTextSize = buttonOptions.TextSize;
            }
            if (buttonOptions.TextFont is not null)
            {
                ButtonTextFont = buttonOptions.TextFont;
            }
            if (buttonOptions.BackgroundColor is not null)
            {
                ButtonBackgroundColor = buttonOptions.BackgroundColor;
            }
            if (buttonOptions.BackgroundColorHover is not null)
            {
                ButtonBackgroundColorHover = buttonOptions.BackgroundColorHover;
            }
            if (buttonOptions.BackgroundColorSelected is not null)
            {
                ButtonBackgroundColorSelected = buttonOptions.BackgroundColorSelected;
            }
            if (buttonOptions.BorderStyle is not null)
            {
                ButtonBorderStyle = buttonOptions.BorderStyle;
            }
            if (buttonOptions.BorderWidth is not null)
            {
                ButtonBorderWidth = buttonOptions.BorderWidth;
            }
            if (buttonOptions.BorderColor is not null)
            {
                ButtonBorderColor = buttonOptions.BorderColor;
            }
            if (buttonOptions.BorderColorHover is not null)
            {
                ButtonBorderColorHover = buttonOptions.BorderColorHover;
            }
            if (buttonOptions.BorderColorSelected is not null)
            {
                ButtonBorderColorSelected = buttonOptions.BorderColorSelected;
            }
            if (buttonOptions.BorderRadius is not null)
            {
                ButtonBorderRadius = buttonOptions.BorderRadius;
            }
        }
    }

    private void GetToolbarOptions()
    {
        var toolbarOptions = _options.GetToolbarOptions();
        if (toolbarOptions is not null)
        {
            if (toolbarOptions.BackgroundColor is not null)
            {
                ToolbarBackgroundColor = toolbarOptions.BackgroundColor;
            }
            if (toolbarOptions.BorderStyle is not null)
            {
                ToolbarBorderStyle = toolbarOptions.BorderStyle;
            }
            if (toolbarOptions.BorderWidth is not null)
            {
                ToolbarBorderWidth = toolbarOptions.BorderWidth;
            }
            if (toolbarOptions.BorderColor is not null)
            {
                ToolbarBorderColor = toolbarOptions.BorderColor;
            }
            if (toolbarOptions.BorderRadius is not null)
            {
                ToolbarBorderRadius = toolbarOptions.BorderRadius;
            }
            if (toolbarOptions.DropdownBackgroundColor is not null)
            {
                ToolbarDropdownBackgroundColor = toolbarOptions.DropdownBackgroundColor;
            }
            if (toolbarOptions.DropdownTextColor is not null)
            {
                ToolbarDropdownTextColor = toolbarOptions.DropdownTextColor;
            }

            if (toolbarOptions.DropdownBackgroundColorHover is not null)
            {
                ToolbarDropdownBackgroundColorHover = toolbarOptions.DropdownBackgroundColorHover;
            }
            if (toolbarOptions.DropdownTextColorHover is not null)
            {
                ToolbarDropdownTextColorHover = toolbarOptions.DropdownTextColorHover;
            }
        }
    }
    private void GetModalOptions()
    {
        var modalOptions = _options.GetModalOptions();
        if (modalOptions is not null)
        {
            if (modalOptions.removeCSSClassInputs is not null)
            {
                MmodalRemoveCSSClassInputs = Convert.ToBoolean(modalOptions.removeCSSClassInputs);
            }
            if (modalOptions.TextColor is not null)
            {
                ModalTextColor = modalOptions.TextColor;
            }
            if (modalOptions.TextSize is not null)
            {
                ModalTextSize = modalOptions.TextSize;
            }
            if (modalOptions.TextFont is not null)
            {
                ModalTextFont = modalOptions.TextFont;
            }
            if (modalOptions.BackgroundColor is not null)
            {
                ModalBackgroundColor = modalOptions.BackgroundColor;
            }
            if (modalOptions.TextboxBackgroundColor is not null)
            {
                ModalTextboxBackgroundColor = modalOptions.TextboxBackgroundColor;
            }
            if (modalOptions.TextboxTextColor is not null)
            {
                ModalTextboxTextColor = modalOptions.TextboxTextColor;
            }
            if (modalOptions.CheckboxAccentColor is not null)
            { 
                ModalCheckboxAccentColor = modalOptions.CheckboxAccentColor;
            }
            if (modalOptions.TextboxBorderColor is not null)
            {
                ModalTextboxBorderColor = modalOptions.TextboxBorderColor;
            }
        }
    }
    #endregion

    #region Fonts
    private List<string> Fonts { get; set; } = new List<string>
    {
        "Ninguno",
        "Arial",
        "Arial Narrow",
        "Baskerville",
        "Brush Script",
        "Calibri",
        "Cambria",
        "Candara",
        "Century Gothic",
        "Claude Garamond",
        "Comic Sans MS",
        "Copperplate",
        "Courier",
        "Didot",
        "Georgia",
        "Gill Sans",
        "Helvetica",
        "Impact",
        "Lucida Bright",
        "Monospace",
        "Optima",
        "Palatino",
        "Segoe UI",
        "Tahoma",
        "Times New Roman",
        "Trebuchet MS",
        "Verdana"
    };
    private async Task Font(string fontName) => await js.InvokeVoidAsync("RTBlazorfied_Method", "font", id, fontName);
    private List<string> Sizes { get; set; } = new List<string>
    {
        "Ninguno",
        "8",
        "9",
        "10",
        "11",
        "12",
        "14",
        "16",
        "18",
        "20",
        "22",
        "24",
        "26",
        "28",
        "36",
        "48",
        "64"
    };
    private async Task Size(string size) => await js.InvokeVoidAsync("RTBlazorfied_Method", "size", id, size == "None" ? size : $"{size}px");
    #endregion
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await Initialize();
    }

    private bool _error;
    public string id = Guid.NewGuid().ToString();
    public string GetShadowId() => $"{id}_Shadow";

    private DotNetObjectReference<RTBlazorfied>? objectReference;
    private async Task Initialize()
    {
        objectReference = DotNetObjectReference.Create(this);
        try
        {
            await js.InvokeVoidAsync("RTBlazorfied_Initialize", id, $"{id}_Shadow", $"{id}_Toolbar", GetStyles(), Value, objectReference);
        }
        catch {
           _error = true;
        }
    }
    public void Dispose() => objectReference?.Dispose();
    protected override async Task OnParametersSetAsync()
    {
        if (!_settingParameter)
        {
            // Throw an error informing the user that a serious error occured.
            try
            {
                await js.InvokeVoidAsync("RTBlazorfied_Method", "loadView", id, Value);
            }
            catch
            {
                _error = true;
            }
        }
    }

    #region Buttons
    private async Task Bold() => await js.InvokeVoidAsync("RTBlazorfied_Method", "bold", id);
    private async Task Italic() => await js.InvokeVoidAsync("RTBlazorfied_Method", "italic", id);
    private async Task Underline() => await js.InvokeVoidAsync("RTBlazorfied_Method", "underline", id);
    private async Task Strikethrough() => await js.InvokeVoidAsync("RTBlazorfied_Method", "strikethrough", id);
    private async Task Subscript() => await js.InvokeVoidAsync("RTBlazorfied_Method", "subscript", id);
    private async Task Superscript() => await js.InvokeVoidAsync("RTBlazorfied_Method", "superscript", id);
    private async Task Alignleft() => await js.InvokeVoidAsync("RTBlazorfied_Method", "alignleft", id);
    private async Task Aligncenter() => await js.InvokeVoidAsync("RTBlazorfied_Method", "aligncenter", id);
    private async Task Alignright() => await js.InvokeVoidAsync("RTBlazorfied_Method", "alignright", id);
    private async Task Alignjustify() => await js.InvokeVoidAsync("RTBlazorfied_Method", "alignjustify", id);
    private async Task Indent() => await js.InvokeVoidAsync("RTBlazorfied_Method", "indent", id);
    private async Task Copy() => await js.InvokeVoidAsync("RTBlazorfied_Method", "copy", id);
    private async Task Cut() => await js.InvokeVoidAsync("RTBlazorfied_Method", "cut", id);
    private async Task Paste() => await js.InvokeVoidAsync("RTBlazorfied_Method", "paste", id);
    private async Task Delete() => await js.InvokeVoidAsync("RTBlazorfied_Method", "delete", id);
    private async Task Selectall() => await js.InvokeVoidAsync("RTBlazorfied_Method", "selectall", id);
    private async Task OrderedList() => await js.InvokeVoidAsync("RTBlazorfied_Method", "orderedlist", id);
    private async Task UnorderedList() => await js.InvokeVoidAsync("RTBlazorfied_Method", "unorderedlist", id);
    private async Task OpenLinkDialog() => await js.InvokeVoidAsync("RTBlazorfied_Method", "openLinkDialog", id);
    private async Task RemoveLink() => await js.InvokeVoidAsync("RTBlazorfied_Method", "removeLink", id);
    private async Task InsertLink() => await js.InvokeVoidAsync("RTBlazorfied_Method", "insertLink", id);
    private async Task CloseDialog(string dialog_id) => await js.InvokeVoidAsync("RTBlazorfied_Method", "closeDialog", id, dialog_id);
    private async Task OpenImageDialog() => await js.InvokeVoidAsync("RTBlazorfied_Method", "openImageDialog", id);
    private async Task UploadImageDialog() => await js.InvokeVoidAsync("RTBlazorfied_Method", "uploadImageDialog", id);
    private async Task InsertImage() => await js.InvokeVoidAsync("RTBlazorfied_Method", "insertImage", id);
    private async Task UploadImage() => await js.InvokeVoidAsync("RTBlazorfied_Method", "uploadImage", id);
    private async Task HandleFileSelected(ChangeEventArgs e)
    {
        var file = e.Value as IBrowserFile;
        if (file != null)
        {
            using var stream = file.OpenReadStream();
            using var memoryStream = new MemoryStream();
            await stream.CopyToAsync(memoryStream);

            var imageBytes = memoryStream.ToArray();
            var Base64Image = Convert.ToBase64String(imageBytes);
        }
    }
    private async Task Undo() => await js.InvokeVoidAsync("RTBlazorfied_Method", "goBack", id);
    private async Task Redo() => await js.InvokeVoidAsync("RTBlazorfied_Method", "goForward", id);
    private async Task OpenBlockQuoteDialog() => await js.InvokeVoidAsync("RTBlazorfied_Method", "openBlockQuoteDialog", id);
    private async Task InsertQuote() => await js.InvokeVoidAsync("RTBlazorfied_Method", "insertBlockQuote", id);
    private async Task OpenCodeBlockDialog() => await js.InvokeVoidAsync("RTBlazorfied_Method", "openCodeBlockDialog", id);
    private async Task InsertCodeBlock() => await js.InvokeVoidAsync("RTBlazorfied_Method", "insertCodeBlock", id);
    private async Task OpenMediaDialog() => await js.InvokeVoidAsync("RTBlazorfied_Method", "openMediaDialog", id);
    private async Task InsertMedia() => await js.InvokeVoidAsync("RTBlazorfied_Method", "insertMedia", id);
    private async Task OpenTextColorDialog() => await js.InvokeVoidAsync("RTBlazorfied_Method", "openTextColorDialog", id);
    private async Task InsertTextColor() => await js.InvokeVoidAsync("RTBlazorfied_Method", "insertTextColor", id);
    private async Task RemoveTextColor() => await js.InvokeVoidAsync("RTBlazorfied_Method", "removeTextColor", id);
    private async Task OpenTextBackgroundColorDialog() => await js.InvokeVoidAsync("RTBlazorfied_Method", "openTextBackgroundColorDialog", id);
    private async Task SelectTextBackgroundColor(string color) => await js.InvokeVoidAsync("RTBlazorfied_Method", "selectTextBackgroundColor", id, color);
    private async Task InsertTextBackgroundColor() => await js.InvokeVoidAsync("RTBlazorfied_Method", "insertTextBackgroundColor", id);
    private async Task FormatText(string format) => await js.InvokeVoidAsync("RTBlazorfied_Method", "format", id, format);
    private async Task OpenDropdown(string dropdown_id) => await js.InvokeVoidAsync("RTBlazorfied_Method", "dropdown", id, dropdown_id);
    private async Task OpenTableDialog() => await js.InvokeVoidAsync("RTBlazorfied_Method", "openTableDialog", id);
    private async Task InsertTable() => await js.InvokeVoidAsync("RTBlazorfied_Method", "insertTable", id);
    private async Task IncreaseIndent() => await js.InvokeVoidAsync("RTBlazorfied_Method", "increaseIndent", id);
    private async Task DecreaseIndent() => await js.InvokeVoidAsync("RTBlazorfied_Method", "decreaseIndent", id);
    public string GetPreviewId() => $"{id}_Preview";
    private async Task OpenPreview() => await js.InvokeVoidAsync("RTBlazorfied_Method", "openPreview", id);
    private async Task ClosePreview() => await js.InvokeVoidAsync("RTBlazorfied_Method", "closePreview", id);
    private async Task OpenCode() => await js.InvokeVoidAsync("RTBlazorfied_Method", "toggleView", id);
    #endregion
}
