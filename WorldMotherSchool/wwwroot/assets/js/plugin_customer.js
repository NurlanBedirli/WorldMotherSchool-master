//============== CKEditor Config==============================


//ClassicEditor
//    .create(document.querySelector('#editor'), {
//        toolbar: ['heading', '|', 'colors', 'bold', 'italic', 'link', 'bulletedList', 'numberedList', 'blockQuote', 'fontfamily','FontFamily'],
//        heading: {
//            options: [
//                { model: 'paragraph', title: 'Paragraph', class: 'ck-heading_paragraph' },
//                { model: 'heading1', view: 'h1', title: 'Heading 1', class: 'ck-heading_heading1' },
//                { model: 'heading2', view: 'h2', title: 'Heading 2', class: 'ck-heading_heading2' }
//            ]
//        },
//        colors: {
//            options: [
//                { model: 'color', title:'color' }
//            ]
//        },
//    })
//    .catch(error => {
//        console.error(error);
//    });

//DecoupledEditor
//    .create(document.querySelector('#editor'))
//    .then(editor => {
//        const toolbarContainer = document.querySelector('#toolbar-container');

//        toolbarContainer.appendChild(editor.ui.view.toolbar.element);
//    })
//    .catch(error => {
//        console.error(error);
//    });

var toolbarOptions = [
    ['bold', 'italic', 'underline', 'strike'],        // toggled buttons
    ['blockquote', 'code-block'],

    [{ 'header': 1 }, { 'header': 2 }],               // custom button values
    [{ 'list': 'ordered' }, { 'list': 'bullet' }],
    [{ 'script': 'sub' }, { 'script': 'super' }],      // superscript/subscript
    [{ 'indent': '-1' }, { 'indent': '+1' }],          // outdent/indent
    [{ 'direction': 'rtl' }],                         // text direction

    [{ 'size': ['small', false, 'large', 'huge'] }],  // custom dropdown
    [{ 'header': [1, 2, 3, 4, 5, 6, false] }],

    [{ 'color': [] }, { 'background': [] }],          // dropdown with defaults from theme
    [{ 'font': [] }],
    [{ 'align': [] }],

    ['clean']                                         // remove formatting button
];

var editor = new Quill('#editor', {
    modules: {
        toolbar: toolbarOptions
    },
    theme: 'snow'
});

var form = document.querySelector('form');

form.onsubmit = function () {
    var about = document.querySelector('input[name=Text]');
    var data = $("#editor").clone();
    var ss = data.children(".ql-editor").html();
    about.value = ss;
}

//form.onsubmit = function () {
//    // Populate hidden form on submit
//    var about = document.querySelector('input[name=about]');
//    about.value = JSON.stringify(quill.getContents());

//    console.log("Submitted", $(form).serialize(), $(form).serializeArray());




