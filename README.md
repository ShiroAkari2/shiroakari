# ShiroAkari's Project — v0.3 Web Demo

This package continues the supplied Unity v0.2 project and adds a browser-playable demo in `WebDemo/`.

## Play locally
Because this demo uses JavaScript ES modules, open it through a local web server (not by double-clicking the HTML file).

### Quick option (Python)
From the `WebDemo` folder:
```bash
python -m http.server 8000
```
Then open `http://localhost:8000`.

### Publish to GitHub Pages
1. Upload/commit the entire project, including the `WebDemo` folder, to your repository.
2. In GitHub, open **Settings → Pages**.
3. Set the publishing source to **Deploy from a branch**.
4. Choose your branch (usually `main`) and folder **/(root)**, then Save.
5. Open `https://YOUR-USERNAME.github.io/YOUR-REPOSITORY/WebDemo/`.

If your existing Pages site is configured to publish from `/docs` or another folder, either change it to root or move the contents of `WebDemo` to the selected publishing folder.

## Browser controls
- WASD / Arrow keys: move
- Mouse: look around (click the game after entering to capture the mouse)
- Shift: run
- Space: jump
- E: interact
- J: journal
- I: satchel
- F5 or Save button: save to this browser/device
- Esc: release mouse / close panel

## Included gameplay
- Third-person 3D meadow made from procedural geometry
- Meadow Keeper dialogue and first quest objective
- Knowledge collectible with an educational fact
- Star Shard inventory collectible
- Ancient rune puzzle that opens the sealed gate
- Journal, quest progress, local save/load

## Notes
- First load requires internet access to fetch Three.js and Google Fonts from their CDNs.
- Browser save data is stored in localStorage on the device/browser where you play.
- This is a playable web demo, not the full open-world game. Models, animation, audio, touch controls, and additional regions are future milestones.
- The Unity v0.2 source remains in `Assets/` and `Packages/`. The web demo is a separate browser implementation of its core gameplay loop.
