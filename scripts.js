import { createClient } from 'https://cdn.jsdelivr.net/npm/@supabase/supabase-js/+esm';

document.addEventListener('DOMContentLoaded', (event) => {
    const supabaseUrl = 'https://ybmiidwzwcofexmyziad.supabase.co';
    // IMPORTANT: The Supabase key below is a placeholder.
    // Replace it with your actual Supabase anonymous key.
    //
    // WARNING: Do not use a service_role key in client-side code.
    // It provides unrestricted access to your database and should be kept secret.
    //
    // For more information on securing your database, please refer to the Supabase documentation:
    // https://supabase.com/docs/guides/auth/row-level-security
    const supabaseKey = 'YOUR_SUPABASE_ANON_KEY';
    const supabase = createClient(supabaseUrl, supabaseKey);

    const checkboxes = document.querySelectorAll('input[type="checkbox"]');
    const progressBar = document.getElementById('progress-bar');
    const backToTopButton = document.getElementById('backToTop');
    const hamburgerMenu = document.getElementById('hamburger-menu');
    const sidebar = document.getElementById('sidebar');

    // Smooth scrolling for anchor links
    document.querySelectorAll('a[href^="#"]').forEach(anchor => {
        anchor.addEventListener('click', function (e) {
            e.preventDefault();
            const targetId = this.getAttribute('href').substring(1);
            const targetElement = document.getElementById(targetId);
            if (targetElement) {
                targetElement.scrollIntoView({
                    behavior: 'smooth'
                });
            }
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

    // Toggle sidebar visibility
    hamburgerMenu.addEventListener('click', () => {
        sidebar.classList.toggle('visible');
    });

    // Scroll to section when sidebar link is clicked
    sidebar.querySelectorAll('a[data-section]').forEach((link, index) => {
        link.addEventListener('click', (e) => {
            e.preventDefault();
            const sectionId = link.getAttribute('data-section');
            const section = document.getElementById(sectionId);
            if (section) {
                section.scrollIntoView({
                    behavior: 'smooth'
                });
                closeHamburgerMenu(); // Close the hamburger menu after link selection
            }
        });
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

    function closeHamburgerMenu() {
        sidebar.classList.remove('visible');
    }
});
