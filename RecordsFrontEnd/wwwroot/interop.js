window.openPdfInBrowser = (pdfContent, fileName) => {
    const blob = new Blob([pdfContent], { type: 'application/pdf' });
    const url = URL.createObjectURL(blob);
    const anchor = document.createElement('a');
    anchor.href = url;
    anchor.download = fileName;
    anchor.target = '_blank';
    anchor.click();
    URL.revokeObjectURL(url);
};