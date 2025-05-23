using assignment_4;
using assignment_4.Models;

var weaponFactory = new WeaponFactory();
var sword = new Sword();
weaponFactory.AddWeapon(Weapons.Sword, sword);

var player1 = new Hero() { Name = "Ram", Strength = 10, Weapon=weaponFactory.GetWeapon(Weapons.Sword) };
var player2 = new Hero() { Name = "Varun", Strength = 12, Weapon = weaponFactory.GetWeapon(Weapons.Sword) };
var goblin = new Goblin();

player1.Attack(goblin);
player2.Attack(goblin);
