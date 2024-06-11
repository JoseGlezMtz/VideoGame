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
        const response = await fetch('/stats/characters_played');
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
        const response = await fetch('/stats/powerup_cards_played');
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

window.addEventListener('load', () => {
    CreateChart();
    CreateChart2();
});
