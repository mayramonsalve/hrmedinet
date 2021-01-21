//////////////////////////////////////////////////////////////////////
// jsPdfWrapper.js
// @usage:
//     1. Include this js, jsPDF and html2canvas library in your head element.
//
//         --------------------------------------------------
//         <head>
//           <script src="https://unpkg.com/jspdf@latest/dist/jspdf.min.js"></script>
//           <script src="https://cdnjs.cloudflare.com/ajax/libs/html2canvas/0.5.0-beta4/html2canvas.min.js"></script>
//           <script type="text/javascript" src="jsPdfWrapper.js"></script>
//         </head>
//         --------------------------------------------------
//
//     2. Instantiate the JsPdfWrapper class in your javascript.
//
//         The easy way is like below.
//         --------------------------------------------------
//         const jsPdfWrapper = new JsPdfWrapper();
//         --------------------------------------------------
//
//         There are some options so you can set an option like below.
//         --------------------------------------------------
//         const options = {
//             'jsPDF': {
//                 'orientation': 'p',
//                 'unit': 'mm',
//                 'format': 'letter',
//             }
//         };
//         const jsPdfWrapper = new JsPdfWrapper(options);
//         --------------------------------------------------
//
//     3. Now you can use it.
//
//         When downloading from HTML.
//         --------------------------------------------------
//         const targetElement = document.getElementById('pdfContainer');
//         const pdfFileName = 'sample';
//         jsPdfWrapper.DownloadFromHTML(targetElement, pdfFileName);
//         --------------------------------------------------
//
//
//////////////////////////////////////////////////////////////////////
class JsPdfWrapper {

    //////////////////////////////////////////////////
    // Constructor
    // @param options object: Toaster Option.
    //////////////////////////////////////////////////
    constructor(options = null) {
        this.OPTIONS = {
            'jsPDF': {
                'orientation': 'p',
                'unit': 'px',
                'format': 'a4',
                'putOnlyUsedFonts': false,
                'compress': false,
                'precision': 2,
                'userUnit': 1.0,
            },
            'html2canvas': {
                'allowTaint': false,
                'backgroundColor': '#ffffff',
                'canvas': null,
                'foreignObjectRendering': false,
                'imageTimeout': 15000,
                'logging': false,
                'onclone': null,
                'proxy': null,
                'removeContainer': true,
                'scale': window.devicePixelRatio,
                'useCORS': false,
            },
        }
        if (options != null) {
            if (options.jsPDF != null) {
                this.OPTIONS.jsPDF = options.jsPDF;
            }
            if (options.html2canvas != null) {
                this.OPTIONS.html2canvas = options.html2canvas;
            }
        }
    }


    //////////////////////////////////////////////////////////////////////
    // Download from HTML.
    //////////////////////////////////////////////////////////////////////
    DownloadFromHTML(targetElm, fileName = null) {
        let totalHeight = targetElm.offsetHeight;
        const pdf = new jsPDF(this.OPTIONS.jsPDF.orientation, this.OPTIONS.jsPDF.unit, this.OPTIONS.jsPDF.format);
        const pdfWidth = pdf.internal.pageSize.width;
        const pdfHeight = pdf.internal.pageSize.height;
        window.scrollTo(0, 0);
        html2canvas(targetElm, this.OPTIONS.html2canvas).then((canvas) => {
            const widthRatio = pdfWidth / canvas.width;
            const sX = 0;
            const sWidth = canvas.width;
            const sHeight = pdfHeight + ((pdfHeight - pdfHeight * widthRatio) / widthRatio);
            const dX = 0;
            const dY = 0;
            const dWidth = sWidth;
            const dHeight = sHeight;
            let pageCnt = 1;
            while (totalHeight > 0) {
                totalHeight -= sHeight;
                let sY = sHeight * (pageCnt - 1);
                const childCanvas = document.createElement('CANVAS');
                childCanvas.setAttribute('width', sWidth);
                childCanvas.setAttribute('height', sHeight);
                const childCanvasCtx = childCanvas.getContext('2d');
                childCanvasCtx.drawImage(canvas, sX, sY, sWidth, sHeight, dX, dY, dWidth, dHeight);
                if (pageCnt > 1) {
                    pdf.addPage();
                }
                pdf.setPage(pageCnt);
                pdf.addImage(childCanvas.toDataURL('image/png'), 'PNG', 0, 0, canvas.width * widthRatio, 0);
                pageCnt++;
            }
            if (fileName == null) {
                fileName = '';
            } else {
                fileName += '_';
            }
            fileName += this.getCurrentDateStr();
            pdf.save(fileName);
        });
        window.scrollTo(0, document.body.scrollHeight || document.documentElement.scrollHeight);
    }


    //////////////////////////////////////////////////////////////////////
    // Get the current date as yyyymmddHHMMSS string.
    //////////////////////////////////////////////////////////////////////
    getCurrentDateStr() {
        const date = new Date();
        const yyyy = date.getFullYear().toString();
        const mm = date.getMonth() + 1 < 10 ? '0' + date.getMonth() + 1 : (date.getMonth() + 1).toString();
        const dd = date.getDate() < 10 ? '0' + date.getDate() : date.getDate().toString();
        const HH = date.getHours() < 10 ? '0' + date.getHours() : date.getHours().toString();
        const MM = date.getMinutes() < 10 ? '0' + date.getMinutes() : date.getMinutes().toString();
        const SS = date.getSeconds() < 10 ? '0' + date.getSeconds() : date.getSeconds().toString();
        return yyyy + mm + dd + HH + MM + SS;
    }


}