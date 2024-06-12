"use strict";

const characterNames = {
    1: "Chef",
    2: "Wendy",
    3: "Brick",
    4: "Lumberjack",
    5: "Coach",
    6: "Lifeguard",
    7: "Mike"
};

function getRandomColor(opacity) {
    const r = Math.floor(Math.random() * 256);
    const g = Math.floor(Math.random() * 256);
    const b = Math.floor(Math.random() * 256);
    return `rgba(${r}, ${g}, ${b}, ${opacity})`;
}

async function CreateChart() {
    try {
        const response = await fetch('http://localhost:5000/stats/characters_played');
        const data = await response.json();

        const characterIds = data.map(entry => entry.character_card_id);
        const values = data.map(entry => entry.amount);

        const labels = characterIds.map(id => characterNames[id]);
        const backgroundColors = characterIds.map(() => getRandomColor(0.6));
        const borderColors = backgroundColors.map(color => color.replace('0.6', '1'));

        const ctx = document.getElementById('myChart').getContext('2d');
        const myChart = new Chart(ctx, {
            type: 'bar',
            data: {
                labels: labels,
                datasets: [{
                    label: 'Characters Played',
                    data: values,
                    backgroundColor: backgroundColors,
                    borderColor: borderColors,
                    borderWidth: 1
                }]
            },
            options: {
                scales: {
                    y: {
                        beginAtZero: true
                    }
                }
            }
        });
    } catch (error) {
        console.error('Error fetching or creating chart:', error);
    }
}

async function CreateChart2() {
    try {
        const response = await fetch('http://localhost:5000/stats/powerup_cards_played');
        const data = await response.json();

        const filteredData = data.filter(entry => entry.amount !== 0);


        const powerupIds = filteredData.map(entry => entry.PU_card_id);
        const values = filteredData.map(entry => entry.amount);

        const labels = powerupIds;
        const backgroundColors = powerupIds.map(() => getRandomColor(0.6));
        const borderColors = backgroundColors.map(color => color.replace('0.6', '1'));

        const ctx = document.getElementById('mySecondChart').getContext('2d');
        const mySecondChart = new Chart(ctx, {
            type: 'bar',
            data: {
                labels: labels,
                datasets: [{
                    label: 'PowerUp Cards Played',
                    data: values,
                    backgroundColor: backgroundColors,
                    borderColor: borderColors,
                    borderWidth: 1
                }]
            },
            options: {
                scales: {
                    y: {
                        beginAtZero: true
                    }
                }
            }
        });
    } catch (error) {
        console.error('Error fetching or creating second chart:', error);
    }
}
async function CreateChart3() {
    try {
        const response = await fetch('http://localhost:5000/stats/Game');
        const data = await response.json();

        const labels = data.map(entry => entry.player_id);
        const values = data.map(entry => entry.num_round);

        const ctx = document.getElementById('myThirdChart').getContext('2d');
        const myThirdChart = new Chart(ctx, {
            type: 'bar',
            data: {
                labels: labels,
                datasets: [{
                    label: 'Total Games Played',
                    data: values,
                    fill: false,
                    borderColor: 'rgb(75, 192, 192)',
                    tension: 0.1
                }]
            },
            options: {
                scales: {
                    y: {
                        beginAtZero: true
                    }
                }
            }
        });
    } catch (error) {
        console.error('Error fetching or creating third chart:', error);
    }

}


window.addEventListener('load', () => {
    CreateChart();
    CreateChart2();
    CreateChart3();
});
