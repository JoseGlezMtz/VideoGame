CharacterData.defaults.font.size = 16;

const ctx = document.getElementById('characterStats').getContext('2d');

const characterStats = new Chart (ctx, {
    type: 'bar',
    data: {
        labels: ['Chef', 'Wnedy', 'Brick', 'Lumberjack', 'Coach', 'Lifeguard',, 'Mike'],
        datasets: [{
            label: 'Card Usage',

        }]
    }
})