import * as THREE from 'three';

const root=document.querySelector('#game');
const scene=new THREE.Scene();
scene.background=new THREE.Color(0x9db8b1);
scene.fog=new THREE.FogExp2(0x9db8b1,.018);
const camera=new THREE.PerspectiveCamera(62,innerWidth/innerHeight,.1,180);
const renderer=new THREE.WebGLRenderer({antialias:true});
renderer.setPixelRatio(Math.min(devicePixelRatio,2));renderer.setSize(innerWidth,innerHeight);
renderer.shadowMap.enabled=true;renderer.shadowMap.type=THREE.PCFSoftShadowMap;
renderer.outputColorSpace=THREE.SRGBColorSpace;renderer.toneMapping=THREE.ACESFilmicToneMapping;renderer.toneMappingExposure=1.12;
root.appendChild(renderer.domElement);

scene.add(new THREE.HemisphereLight(0xdceeea,0x52634b,2.0));
const sun=new THREE.DirectionalLight(0xffe4b8,3.0);sun.position.set(-22,35,-15);sun.castShadow=true;sun.shadow.mapSize.set(2048,2048);sun.shadow.camera.left=-35;sun.shadow.camera.right=35;sun.shadow.camera.top=35;sun.shadow.camera.bottom=-35;scene.add(sun);
const mat=(color,roughness=.9,metalness=0)=>new THREE.MeshStandardMaterial({color,roughness,metalness});
const groundMat=mat(0x63875b),trunkMat=mat(0x604a36),leafMats=[mat(0x426d4d),mat(0x527b51),mat(0x66875a)],stoneMat=mat(0x858b83),mossMat=mat(0x70896a),goldMat=new THREE.MeshStandardMaterial({color:0xe8c778,emissive:0x9b6d22,emissiveIntensity:.65,roughness:.35}),blueGlow=new THREE.MeshStandardMaterial({color:0x9ce9ed,emissive:0x39c7d5,emissiveIntensity:2.1,roughness:.25});
const ground=new THREE.Mesh(new THREE.PlaneGeometry(120,120),groundMat);ground.rotation.x=-Math.PI/2;ground.receiveShadow=true;scene.add(ground);
const floorPatch=new THREE.Mesh(new THREE.CircleGeometry(13,64),mat(0x789368));floorPatch.rotation.x=-Math.PI/2;floorPatch.position.y=.015;floorPatch.scale.set(1.15,1,.85);scene.add(floorPatch);
function addMesh(geo,material,pos,scale,cast=true){const m=new THREE.Mesh(geo,material);m.position.set(...pos);if(scale)m.scale.set(...scale);m.castShadow=cast;m.receiveShadow=true;scene.add(m);return m;}
function tree(x,z,s=1){addMesh(new THREE.CylinderGeometry(.22*s,.38*s,2.5*s,7),trunkMat,[x,1.25*s,z],[1,1,1]);let crown=addMesh(new THREE.IcosahedronGeometry(1.5*s,1),leafMats[Math.floor(Math.random()*leafMats.length)],[x,3*s,z],[1,1.05,1]);crown.rotation.y=Math.random()*3;}
for(let i=0;i<85;i++){let a=Math.random()*Math.PI*2,r=14+Math.random()*28;tree(Math.cos(a)*r,Math.sin(a)*r,.65+Math.random()*.8);}
for(let i=0;i<36;i++){let x=(Math.random()-.5)*27,z=(Math.random()-.5)*28;if(Math.hypot(x,z)<5)continue;let b=addMesh(new THREE.DodecahedronGeometry(.55+Math.random()*.65,0),i%3?stoneMat:mossMat,[x,.35,z],[1,.75,.85]);b.rotation.set(Math.random(),Math.random(),Math.random());}
const flowerMat=mat(0xe6c9a2),flowerCenter=goldMat;
for(let i=0;i<100;i++){let x=(Math.random()-.5)*30,z=(Math.random()-.5)*30;if(Math.hypot(x,z)<4)continue;addMesh(new THREE.SphereGeometry(.07,8,6),flowerMat,[x,.15,z],[1,.65,1],false);}

// Distant mountains and a broken stone arch
for(let i=0;i<16;i++){let a=i/16*Math.PI*2,r=43,h=7+Math.random()*8;let m=addMesh(new THREE.ConeGeometry(5,h,5),mat(i%2?0x71877e:0x7f9185),[Math.cos(a)*r,h/2-1,Math.sin(a)*r],[1,1,1],false);m.rotation.y=a;}
const archMat=mat(0x777e78);for(const x of [-2.3,2.3])addMesh(new THREE.BoxGeometry(.8,4,.8),archMat,[x,2,13]);addMesh(new THREE.BoxGeometry(5.4,.8,.9),archMat,[0,4,13]);
for(let i=0;i<5;i++){let p=addMesh(new THREE.BoxGeometry(.65,.35,.7),mat(0x99998a),[-2+i,4.65,13]);p.rotation.z=(Math.random()-.5)*.25;}

// Player capsule
const player=new THREE.Group();scene.add(player);player.position.set(0,0,0);
const bodyMat=mat(0x536e91,.75),coatMat=mat(0xd7d0bd),skinMat=mat(0xe7c5a3),hairMat=mat(0x342d3c);
const torso=addMesh(new THREE.CapsuleGeometry(.38,.7,5,10),coatMat,[0,1.05,0],[.9,1,0.62]);torso.parent=player;scene.remove(torso);player.add(torso);
const head=addMesh(new THREE.SphereGeometry(.29,16,12),skinMat,[0,1.85,0],[.9,1, .9]);head.parent=player;scene.remove(head);player.add(head);
const hair=addMesh(new THREE.SphereGeometry(.31,12,10),hairMat,[0,2.02,-.04],[1, .75,1]);hair.parent=player;scene.remove(hair);player.add(hair);
const cape=addMesh(new THREE.ConeGeometry(.48,.9,8),bodyMat,[0,1.12,-.35],[1,1, .45]);cape.rotation.x=-.18;cape.parent=player;scene.remove(cape);player.add(cape);
const legs=[];for(let x of [-.17,.17]){let l=addMesh(new THREE.CapsuleGeometry(.12,.55,4,8),bodyMat,[x,.43,0],[1,1,1]);l.parent=player;scene.remove(l);player.add(l);legs.push(l);}
const shadow=addMesh(new THREE.CircleGeometry(.55,20),new THREE.MeshBasicMaterial({color:0x203126,transparent:true,opacity:.23}),[0,.025,0],[1,.5,1],false);shadow.parent=player;scene.remove(shadow);player.add(shadow);

// Interaction objects with proximity triggers
const interactables=[];
function addInteractable(kind,title,pos,mesh,range=3){interactables.push({kind,title,pos:new THREE.Vector3(...pos),mesh,range,done:false});return interactables[interactables.length-1];}
const leaf=addMesh(new THREE.IcosahedronGeometry(.42,1),new THREE.MeshStandardMaterial({color:0x8acb82,emissive:0x2e7a37,emissiveIntensity:.7,roughness:.3}),[3,.95,2],[1,.42,1]);const leafObj=addInteractable('knowledge','Ancient Leaf Specimen',[3,0,2],leaf);
const shard=addMesh(new THREE.OctahedronGeometry(.48),blueGlow,[-3,.9,2],[1,1.25,1]);const shardObj=addInteractable('shard','Star Shard',[-3,0,2],shard);
const npc=new THREE.Group();scene.add(npc);npc.position.set(-5,0,4);
const npcBody=addMesh(new THREE.CapsuleGeometry(.38,.75,5,10),mat(0xb58d5d),[0,1,0]);npcBody.parent=npc;scene.remove(npcBody);npc.add(npcBody);
const npcHead=addMesh(new THREE.SphereGeometry(.29,16,12),skinMat,[0,1.85,0]);npcHead.parent=npc;scene.remove(npcHead);npc.add(npcHead);
const npcHat=addMesh(new THREE.ConeGeometry(.43,.55,8),mat(0x536b59),[0,2.22,0]);npcHat.parent=npc;scene.remove(npcHat);npc.add(npcHat);
const npcObj=addInteractable('npc','Speak with the Meadow Keeper',[-5,0,4],npc);
const runeBase=addMesh(new THREE.CylinderGeometry(.85,1.1,.55,8),stoneMat,[0,.28,10]);
const runeRing=addMesh(new THREE.TorusGeometry(.62,.09,8,32),goldMat,[0,1.05,10]);runeRing.rotation.x=Math.PI/2;
const runeCore=addMesh(new THREE.OctahedronGeometry(.42),blueGlow,[0,1.1,10]);
const runeObj=addInteractable('rune','Restore the ancient seal',[0,0,10],runeCore,3.7);
const gate=new THREE.Group();scene.add(gate);gate.position.set(0,1.7,14);
const gateMat=mat(0x4b5260,.65);for(let x=-1.5;x<=1.5;x+=1.5){let col=addMesh(new THREE.BoxGeometry(.28,3.4,.5),gateMat,[x,0,0]);col.parent=gate;scene.remove(col);gate.add(col);}
const gateBar=addMesh(new THREE.BoxGeometry(3.3,.3,.55),goldMat,[0,1.3,0]);gateBar.parent=gate;scene.remove(gateBar);gate.add(gateBar);
const gateObj=addInteractable('gate','The sealed path', [0,0,14],gate,4);gateObj.done=true;

// Controls and state
const keys={};let yaw=0,pitch=.24,locked=false,started=false,velocityY=0,onGround=true,active=null;
let journal=[],inventory={},quest={keeper:false,leaf:false,rune:false,shard:false};
const storageKey='shiroakari_web_v03';
function toast(msg){const el=document.querySelector('#toast');el.textContent=msg;el.classList.add('show');clearTimeout(toast.t);toast.t=setTimeout(()=>el.classList.remove('show'),2500);}
function updateQuest(){let n=(quest.keeper?1:0)+(quest.leaf?1:0)+(quest.rune?1:0);document.querySelector('#questProgress').style.width=(n/3*100)+'%';document.querySelector('#questStep').textContent=n+' / 3 discoveries';let t='Find the Meadow Keeper and learn about the ancient seal.';if(quest.keeper&&!quest.leaf)t='Investigate the glowing leaf and record its secret.';if(quest.keeper&&quest.leaf&&!quest.rune)t='Find the rune pedestal and restore the ancient seal.';if(quest.rune)t='The sealed path is open. Wander onward and discover the meadow.';document.querySelector('#objective').textContent=t;}
function addJournal(id,title,category,body){if(journal.some(x=>x.id===id))return;journal.push({id,title,category,body});renderJournal();toast('Journal updated · '+title);}
function renderJournal(){document.querySelector('#panelBody').innerHTML=journal.length?journal.map(e=>`<article class="entry"><div class="tag">${e.category}</div><h3>${e.title}</h3><p>${e.body}</p></article>`).join(''):'<p class="empty">Your journal is empty. Explore the meadow and interact with objects that shimmer with a little magic.</p>';}
function renderBag(){let rows=Object.entries(inventory);document.querySelector('#panelBody').innerHTML=rows.length?rows.map(([k,v])=>`<article class="entry"><div class="tag">COLLECTIBLE</div><h3>${k} <span style="color:#e5c98e">× ${v}</span></h3><p>A small keepsake from your journey through the Whispering Meadow.</p></article>`).join(''):'<p class="empty">Your satchel is empty. A curious little treasure may be waiting nearby.</p>';}
function openPanel(type){document.querySelector('#panel').classList.remove('hidden');document.querySelector('#panelEyebrow').textContent=type==='journal'?'FIELD NOTES':'YOUR BELONGINGS';document.querySelector('#panelTitle').textContent=type==='journal'?'Knowledge Journal':'Traveler’s Satchel';type==='journal'?renderJournal():renderBag();}
function closePanel(){document.querySelector('#panel').classList.add('hidden');}
function interact(o){if(!o)return;
 if(o.kind==='knowledge'&&!o.done){o.done=true;o.mesh.visible=false;quest.leaf=true;addJournal('leaf','Why are leaves green?','Nature','Leaves look green because chlorophyll absorbs mostly red and blue light, while reflecting more green light back to our eyes.');updateQuest();}
 if(o.kind==='shard'&&!o.done){o.done=true;o.mesh.visible=false;inventory['Star Shard']=(inventory['Star Shard']||0)+1;quest.shard=true;toast('Collected · Star Shard × 1');}
 if(o.kind==='npc'){quest.keeper=true;updateQuest();document.querySelector('#panelEyebrow').textContent='MEADOW KEEPER';document.querySelector('#panelTitle').textContent='A voice among the trees';document.querySelector('#panelBody').innerHTML='<article class="entry"><div class="tag">THE KEEPER’S TALE</div><h3>“The meadow remembers.”</h3><p>Welcome, traveler. Long ago, this place was watched over by the stars. A leaf keeps the memory of sunlight, and the old rune keeps the memory of a promise. Find the glowing leaf, then wake the seal beyond these stones.</p></article><p class="empty">The Keeper smiles softly. “There is no hurry. Wonder is a kind of journey.”</p>';document.querySelector('#panel').classList.remove('hidden');}
 if(o.kind==='rune'&&!o.done){o.done=true;quest.rune=true;runeRing.material=goldMat;runeCore.material=goldMat;gate.visible=false;gateObj.done=true;updateQuest();toast('The ancient seal awakens. The path is open!');addJournal('seal','The promise of the old seal','Mystery','The rune responds to a quiet touch. Its light reveals that the old gate was never a prison, but a promise to protect the meadow.');}
 if(o.kind==='gate'&&quest.rune){toast('The path beyond the arch is open. Keep exploring!');}
}
function save(){let data={position:player.position.toArray(),yaw,pitch,journal,inventory,quest,leafDone:leafObj.done,shardDone:shardObj.done,runeDone:runeObj.done};localStorage.setItem(storageKey,JSON.stringify(data));toast('Journey saved on this device.');}
function load(){try{let d=JSON.parse(localStorage.getItem(storageKey)||'null');if(!d)return false;player.position.fromArray(d.position||[0,0,0]);yaw=d.yaw||0;pitch=d.pitch||.24;journal=d.journal||[];inventory=d.inventory||{};quest={...quest,...d.quest};leafObj.done=!!d.leafDone;leaf.visible=!leafObj.done;shardObj.done=!!d.shardDone;shard.visible=!shardObj.done;runeObj.done=!!d.runeDone;gate.visible=!runeObj.done;updateQuest();return true;}catch(e){console.warn('Save could not be loaded',e);return false;}}
if(load())toast('Saved journey restored.');
function onKey(e,down){keys[e.code]=down;if(down){if(e.code==='KeyE')interact(active);if(e.code==='KeyJ')openPanel('journal');if(e.code==='KeyI')openPanel('bag');if(e.code==='Escape')document.exitPointerLock?.();if(e.code==='F5'){e.preventDefault();save();}}}
addEventListener('keydown',e=>onKey(e,true));addEventListener('keyup',e=>onKey(e,false));
renderer.domElement.addEventListener('click',()=>{if(started&&!document.querySelector('#startScreen').classList.contains('gone')===false)renderer.domElement.requestPointerLock?.();});
document.addEventListener('pointerlockchange',()=>locked=document.pointerLockElement===renderer.domElement);
document.addEventListener('mousemove',e=>{if(locked){yaw-=e.movementX*.0022;pitch=Math.max(-.05,Math.min(.8,pitch+e.movementY*.0018));}});
document.querySelector('#startBtn').addEventListener('click',()=>{started=true;document.querySelector('#startScreen').classList.add('gone');renderer.domElement.requestPointerLock?.();toast('Welcome to the Whispering Meadow.');});
document.querySelector('#journalBtn').onclick=()=>openPanel('journal');document.querySelector('#bagBtn').onclick=()=>openPanel('bag');document.querySelector('#saveBtn').onclick=save;document.querySelector('#closePanel').onclick=closePanel;
addEventListener('keydown',e=>{if(e.code==='Escape')closePanel();});
let last=performance.now();
const clock=new THREE.Clock();
function update(dt,t){
 const forward=new THREE.Vector3(-Math.sin(yaw),0,-Math.cos(yaw));const right=new THREE.Vector3(Math.cos(yaw),0,-Math.sin(yaw));
 let move=new THREE.Vector3();if(keys.KeyW||keys.ArrowUp)move.add(forward);if(keys.KeyS||keys.ArrowDown)move.sub(forward);if(keys.KeyD||keys.ArrowRight)move.add(right);if(keys.KeyA||keys.ArrowLeft)move.sub(right);
 if(move.lengthSq()>0){move.normalize();player.position.addScaledVector(move,(keys.ShiftLeft?7:4)*dt);player.rotation.y=Math.atan2(move.x,move.z);legs.forEach((l,i)=>l.rotation.x=Math.sin(t*10+i*Math.PI)*.45);}else legs.forEach(l=>l.rotation.x=0);
 if(keys.Space&&onGround){velocityY=6.4;onGround=false;}velocityY-=18*dt;player.position.y+=velocityY*dt;if(player.position.y<=0){player.position.y=0;velocityY=0;onGround=true;}
 player.position.x=THREE.MathUtils.clamp(player.position.x,-48,48);player.position.z=THREE.MathUtils.clamp(player.position.z,-48,48);
 const target=new THREE.Vector3(player.position.x+Math.sin(yaw)*Math.cos(pitch)*6,player.position.y+2.2+Math.sin(pitch)*6,player.position.z+Math.cos(yaw)*Math.cos(pitch)*6);
 camera.position.lerp(target,1-Math.exp(-7*dt));camera.lookAt(player.position.x,player.position.y+1.15,player.position.z);
 leaf.rotation.y+=dt*.8;leaf.position.y=1+Math.sin(t*2)*.13;shard.rotation.y+=dt*1.2;shard.rotation.x+=dt*.7;shard.position.y=.9+Math.sin(t*2.3)*.18;runeCore.rotation.y+=dt*.8;runeRing.rotation.z+=dt*.25;
 npc.rotation.y=Math.sin(t*.5)*.12;
 active=null;let best=99;for(const o of interactables){if(o.done&&o.kind!=='gate')continue;let d=player.position.distanceTo(o.pos);if(d<o.range&&d<best){best=d;active=o;}}
 const prompt=document.querySelector('#prompt');if(active){prompt.classList.remove('hidden');document.querySelector('#promptText').textContent=active.kind==='gate'&&!quest.rune?'The path is sealed':active.title;}else prompt.classList.add('hidden');
}
function animate(){requestAnimationFrame(animate);const dt=Math.min(clock.getDelta(),.04),t=clock.elapsedTime;update(dt,t);renderer.render(scene,camera);}
animate();
addEventListener('resize',()=>{camera.aspect=innerWidth/innerHeight;camera.updateProjectionMatrix();renderer.setSize(innerWidth,innerHeight);});
document.querySelector('#loading').style.display='none';
