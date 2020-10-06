/**
 * @license Copyright (c) 2003-2020, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see https://ckeditor.com/legal/ckeditor-oss-license
 */

CKEDITOR.editorConfig = function( config ) {
	// Define changes to default configuration here. For example:
	// config.language = 'fr';
	// config.uiColor = '#AADC6E';
	config.defaultLanguage = 'pt-br';
	config.extraPlugins = 'wordcount,notification,exportpdf'; 
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

	config.toolbarGroups = [
		{ name: 'document', groups: [ 'mode', 'document', 'doctools' ] },
		{ name: 'clipboard', groups: [ 'clipboard', 'undo' ] },
		{ name: 'editing', groups: [ 'find', 'selection', 'spellchecker', 'editing' ] },
		{ name: 'forms', groups: [ 'forms' ] },
		'/',
		{ name: 'basicstyles', groups: [ 'basicstyles', 'cleanup' ] },
		{ name: 'paragraph', groups: [ 'list', 'indent', 'blocks', 'align', 'bidi', 'paragraph' ] },
		{ name: 'links', groups: [ 'links' ] },
		{ name: 'insert', groups: [ 'insert' ] },
		'/',
		{ name: 'styles', groups: [ 'styles' ] },
		{ name: 'colors', groups: [ 'colors' ] },
		{ name: 'tools', groups: [ 'tools' ] },
		{ name: 'others', groups: [ 'others' ] },
		{ name: 'about', groups: [ 'about' ] }
	];

	config.removeButtons = 'ExportPdf,Form,Iframe,ShowBlocks,Flash,CreateDiv,Radio,Checkbox,TextField,Textarea,Select,Button,ImageButton,HiddenField,About';

};
