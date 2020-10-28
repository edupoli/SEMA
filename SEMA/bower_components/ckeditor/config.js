/**
 * @license Copyright (c) 2003-2020, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see https://ckeditor.com/legal/ckeditor-oss-license
 */

CKEDITOR.editorConfig = function( config ) {
  // Define changes to default configuration here. For example:
  // config.language = 'fr';
  // config.uiColor = '#AADC6E';

config.toolbarGroups = [
    { name: 'document', groups: [ 'mode', 'document', 'doctools' ] },
    { name: 'clipboard', groups: [ 'clipboard', 'undo' ] },
    { name: 'basicstyles', groups: [ 'basicstyles', 'cleanup' ] },
    { name: 'editing', groups: [ 'find', 'selection', 'spellchecker', 'editing' ] },
    { name: 'forms', groups: [ 'forms' ] },
    { name: 'paragraph', groups: [ 'list', 'indent', 'blocks', 'align', 'bidi', 'paragraph' ] },
    { name: 'links', groups: [ 'links' ] },
    { name: 'insert', groups: [ 'insert' ] },
    { name: 'styles', groups: [ 'styles' ] },
    { name: 'colors', groups: [ 'colors' ] },
    { name: 'tools', groups: [ 'tools' ] },
    { name: 'others', groups: [ 'others' ] },
    { name: 'about', groups: [ 'about' ] }
  ];

  config.removeButtons = 'Source,Save,NewPage,ExportPdf,Preview,Templates,Replace,Find,SelectAll,Scayt,Form,Checkbox,Radio,TextField,Textarea,Select,Button,ImageButton,HiddenField,Subscript,Superscript,Outdent,Indent,Blockquote,CreateDiv,BidiLtr,BidiRtl,Language,Anchor,Flash,HorizontalRule,PageBreak,Iframe,About,ShowBlocks,Maximize';
  
  config.defaultLanguage = 'pt-br';
  //config.editorplaceholder = 'Type your comment…';
  config.extraPlugins = 'wordcount,notification,autosave,editorplaceholder'; 
  
  config.wordcount = {

    // Whether or not you want to show the Paragraphs Count
    showParagraphs: false,

    // Whether or not you want to show the Word Count
    showWordCount: false,

    // Whether or not you want to show the Char Count
    showCharCount: true,

    // Whether or not you want to count Spaces as Chars
    countSpacesAsChars: true,

    // Whether or not to include Html chars in the Char Count
    countHTML: false,
    
    // Maximum allowed Word Count, -1 is default for unlimited
    maxWordCount: -1,

    // Maximum allowed Char Count, -1 is default for unlimited
    maxCharCount: -1,

    // Add filter to add or remove element before counting (see CKEDITOR.htmlParser.filter), Default value : null (no filter)
    filter: new CKEDITOR.htmlParser.filter({
        elements: {
            div: function( element ) {
                if(element.attributes.class == 'mediaembed') {
                    return false;
                }
            }
        }
    })
};

config.autosave = { 
      // Auto save Key - The Default autosavekey can be overridden from the config ...
      //Savekey : 'autosave_' + window.location + "_" + $('#' + <%=descricao.ClientID%>.name).attr('name'),

      // Ignore Content older then X
      //The Default Minutes (Default is 1440 which is one day) after the auto saved content is ignored can be overidden from the config ...
      NotOlderThen : 1440,

      // Save Content on Destroy - Setting to Save content on editor destroy (Default is false) ...
      saveOnDestroy : false,

      // Setting to set the Save button to inform the plugin when the content is saved by the user and doesn't need to be stored temporary ...
      saveDetectionSelectors : "a[href^='javascript:__doPostBack'][id*='Save'],a[id*='Cancel']",

      // Notification Type - Setting to set the if you want to show the "Auto Saved" message, and if yes you can show as Notification or as Message in the Status bar (Default is "notification")
     // messageType : "notification",

     // Show in the Status Bar
     messageType : "statusbar",

     // Show no Message
     //messageType : "no",

     // Delay
     delay : 3,

     // The Default Diff Type for the Compare Dialog, you can choose between "sideBySide" or "inline". Default is "sideBySide"
     diffType : "sideBySide",

     // autoLoad when enabled it directly loads the saved content
     autoLoad: true
};  

};


