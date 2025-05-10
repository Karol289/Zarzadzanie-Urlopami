window.exportToPdf = async function (elementId, title) {
    // Create loading overlay
    const loadingElement = document.createElement('div');
    loadingElement.classList.add('pdf-loading');
    loadingElement.style.position = 'fixed';
    loadingElement.style.top = '0';
    loadingElement.style.left = '0';
    loadingElement.style.width = '100%';
    loadingElement.style.height = '100%';
    loadingElement.style.backgroundColor = 'rgba(0,0,0,0.5)';
    loadingElement.style.display = 'flex';
    loadingElement.style.justifyContent = 'center';
    loadingElement.style.alignItems = 'center';
    loadingElement.style.zIndex = '9999';
    loadingElement.innerHTML = '<div style="background-color: white; padding: 20px; border-radius: 5px; font-weight: bold;">Przygotowywanie raportu PDF...</div>';
    document.body.appendChild(loadingElement);
    
    try {
        // Load required libraries
        await loadScript('https://cdnjs.cloudflare.com/ajax/libs/html2canvas/1.4.1/html2canvas.min.js');
        await loadScript('https://cdnjs.cloudflare.com/ajax/libs/jspdf/2.5.1/jspdf.umd.min.js');
        
        // Get the report element
        const element = document.getElementById(elementId);
        if (!element) {
            throw new Error('Element do eksportu nie zosta³ znaleziony');
        }

        // Create a specific wrapper for printing that will ensure proper styling
        const printWrapper = document.createElement('div');
        printWrapper.innerHTML = `
            <div style="width: 800px; padding: 20px; background: white; font-family: Arial, sans-serif;">
                <h2 style="text-align: center; margin-bottom: 20px;">${title}</h2>
                ${element.outerHTML}
            </div>
        `;
        
        // Prepare the cloned content for PDF
        const tableElement = printWrapper.querySelector('table');
        if (tableElement) {
            // Add borders for table
            tableElement.style.borderCollapse = 'collapse';
            tableElement.style.width = '100%';
            
            // Style table headers
            const thElements = tableElement.querySelectorAll('th');
            thElements.forEach(th => {
                th.style.backgroundColor = '#0275d8';
                th.style.color = 'white';
                th.style.padding = '8px';
                th.style.border = '1px solid #ddd';
                th.style.textAlign = 'left';
            });
            
            // Style table cells
            const tdElements = tableElement.querySelectorAll('td');
            tdElements.forEach(td => {
                td.style.padding = '8px';
                td.style.border = '1px solid #ddd';
                td.style.fontSize = '12px';
            });

            // Fix Polish characters in element text
            fixPolishCharacters(printWrapper);
            
            // Style badges
            const badges = printWrapper.querySelectorAll('.badge');
            badges.forEach(badge => {
                const classes = badge.className;
                badge.style.display = 'inline-block';
                badge.style.padding = '3px 6px';
                badge.style.fontSize = '10px';
                badge.style.fontWeight = 'bold';
                badge.style.borderRadius = '3px';
                badge.style.textAlign = 'center';
                
                if (classes.includes('bg-success')) {
                    badge.style.backgroundColor = '#28a745';
                    badge.style.color = 'white';
                } else if (classes.includes('bg-danger')) {
                    badge.style.backgroundColor = '#dc3545';
                    badge.style.color = 'white';
                } else {
                    badge.style.backgroundColor = '#ffc107';
                    badge.style.color = 'black';
                }
            });
        }
        
        // Add to DOM temporarily
        printWrapper.style.position = 'absolute';
        printWrapper.style.left = '-9999px';
        document.body.appendChild(printWrapper);
        
        // Wait for fonts and images to load
        await new Promise(resolve => setTimeout(resolve, 1000));
        
        // Create canvas from the prepared element
        const canvas = await html2canvas(printWrapper, {
            scale: 2,
            useCORS: true,
            logging: false,
            allowTaint: true,
            onclone: function(clonedDoc) {
                const clonedElement = clonedDoc.querySelector(`#${elementId}`);
                if (clonedElement) {
                    // Additional styling for the cloned document if needed
                    const listItems = clonedElement.querySelectorAll('li');
                    listItems.forEach(item => {
                        item.style.listStyleType = 'none';
                        item.style.margin = '2px 0';
                        item.style.padding = '2px 0';
                    });
                }
            }
        });
        
        // Remove temporary element
        document.body.removeChild(printWrapper);
        
        // Create PDF
        const { jsPDF } = window.jspdf;
        const pdf = new jsPDF({
            orientation: 'p',
            unit: 'pt',
            format: 'a4',
            compress: true
        });
        
        // Calculate dimensions
        const imgWidth = pdf.internal.pageSize.getWidth() - 40; // 20pt margin on each side
        const imgHeight = (canvas.height * imgWidth) / canvas.width;
        
        // Add the canvas as an image to the PDF
        pdf.addImage(
            canvas, 
            'PNG', 
            20, // x position
            20, // y position
            imgWidth,
            imgHeight
        );
        
        // Save the PDF
        pdf.save(`Raport_${title.replace(/\s+/g, '_').replace(/_-_/g, '_')}.pdf`);
        
    } catch (error) {
        console.error('B³¹d podczas generowania PDF:', error);
        alert('Wyst¹pi³ b³¹d podczas generowania pliku PDF. Spróbuj ponownie.');
    } finally {
        document.body.removeChild(loadingElement);
    }
};

// Helper function to load scripts
function loadScript(url) {
    return new Promise((resolve, reject) => {
        if (document.querySelector(`script[src="${url}"]`)) {
            resolve();
            return;
        }
        
        const script = document.createElement('script');
        script.src = url;
        script.onload = resolve;
        script.onerror = reject;
        document.head.appendChild(script);
    });
}

// Helper function to handle Polish characters
function fixPolishCharacters(element) {
    const walker = document.createTreeWalker(
        element,
        NodeFilter.SHOW_TEXT,
        null,
        false
    );

    let node;
    while (node = walker.nextNode()) {
        // If this node is not inside a script or style tag
        if (!['SCRIPT', 'STYLE'].includes(node.parentNode.tagName)) {
            // We don't need to do anything special for Polish characters
            // since we're using html2canvas with foreignObjectRendering enabled
            // Just ensure text nodes are properly encoded in UTF-8 if needed
            if (node.textContent && node.textContent.trim() !== '') {
                // Make sure text is clean UTF-8 (though modern browsers handle this well)
                const text = node.textContent;
                node.textContent = text;
            }
        }
    }
}