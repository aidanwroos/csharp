import json
from collections import Counter

data = []
with open('rounds.json') as f:
    for line in f:
        data.append(json.loads(line.strip()))

# split into individual games by detecting when roundNumber resets
game_lengths = []
current_len = 0
prev = -1
for r in data:
    if r['roundNumber'] < prev:
        game_lengths.append(current_len)
        current_len = 1
    else:
        current_len += 1
    prev = r['roundNumber']
game_lengths.append(current_len)

print(f'Total rounds: {len(data)}')
print(f'Games detected: {len(game_lengths)}')
print(f'Avg rounds per game: {sum(game_lengths) / len(game_lengths):.1f}')
print(f'Min rounds in a game: {min(game_lengths)}')
print(f'Max rounds in a game: {max(game_lengths)}')

wins = Counter(r['winner'] for r in data)
pot_dist = Counter(r['potSize'] for r in data)
war_dist = Counter(r['warCount'] for r in data)

print('Wins:', dict(wins))
print('Pot distribution:', dict(sorted(pot_dist.items())))
print('War count distribution:', dict(sorted(war_dist.items())))