import { createClient } from 'https://cdn.jsdelivr.net/npm/@supabase/supabase-js/+esm';

document.addEventListener('DOMContentLoaded', (event) => {
    const supabaseUrl = 'https://ybmiidwzwcofexmyziad.supabase.co';
    const supabaseKey = 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6InlibWlpZHd6d2NvZmV4bXl6aWFkIiwicm9sZSI6InNlcnZpY2Vfcm9sZSIsImlhdCI6MTczNzM3ODYzNSwiZXhwIjoyMDUyOTU0NjM1fQ.F4zSCQLRKcBULRV6yRb2HX_k0-RptBj9UcgIfn0ni8Q';
    const supabase = createClient(supabaseUrl, supabaseKey);

    const checkboxes = document.querySelectorAll('input[type="checkbox"]');
    const progressBar = document.getElementById('progress-bar');
    const backToTopButton = document.getElementById('backToTop');

    // Smooth scrolling for anchor links
    document.querySelectorAll('a[href^="#"]').forEach(anchor => {
        anchor.addEventListener('click', function (e) {
            e.preventDefault();
            document.querySelector(this.getAttribute('href')).scrollIntoView({
                behavior: 'smooth'
            });
        });
    });

    // Scroll animations
    const observer = new IntersectionObserver(entries => {
        entries.forEach(entry => {
            if (entry.isIntersecting) {
                entry.target.classList.add('animate');
            }
        });
    });

    document.querySelectorAll('.phase-section').forEach(section => {
        observer.observe(section);
    });

    // Back to top button
    backToTopButton.addEventListener('click', () => {
        window.scrollTo({
            top: 0,
            behavior: 'smooth'
        });
    });

    window.addEventListener('scroll', () => {
        if (window.scrollY > 300) {
            backToTopButton.style.display = 'block';
        } else {
            backToTopButton.style.display = 'none';
        }
    });

    // Load checkbox states from the server
    supabase
        .from('checkboxes')
        .select('*')
        .then(response => {
            console.log('Loaded checkbox states:', response.data);
            response.data.forEach(item => {
                const checkbox = document.getElementById(item.id);
                if (checkbox) {
                    checkbox.checked = item.checked;
                }
            });
            updateProgressBar();
        })
        .catch(error => {
            console.error('Error loading checkbox states:', error);
        });

    // Save checkbox state to the server
    checkboxes.forEach(checkbox => {
        checkbox.addEventListener('change', (event) => {
            const id = event.target.id;
            const checked = event.target.checked;

            supabase
                .from('checkboxes')
                .upsert({ id, checked })
                .then(response => {
                    console.log(`Checkbox with id ${id} is now ${checked ? 'checked' : 'unchecked'} and saved to the database.`);
                    updateProgressBar();
                    if (checked) {
                        triggerFireworks();
                    }
                })
                .catch(error => {
                    console.error('Error saving checkbox state:', error);
                });
        });
    });

    function updateProgressBar() {
        const totalCheckboxes = checkboxes.length;
        const checkedCheckboxes = document.querySelectorAll('input[type="checkbox"]:checked').length;
        const progress = (checkedCheckboxes / totalCheckboxes) * 100;
        progressBar.style.width = `${progress}%`;
        progressBar.innerText = `${Math.round(progress)}%`;
        progressBar.setAttribute('title', `${checkedCheckboxes} out of ${totalCheckboxes} completed`);
    }

    function triggerFireworks() {
        confetti({
            particleCount: 100,
            spread: 70,
            origin: { y: 0.6 }
        });
    }
});









