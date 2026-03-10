namespace Minimal_API
{
    public static class Anchors
    {
        public static string Pick(int count)
        {
            var random = new Random();
            var selectedAnchors = new HashSet<string>();
            while (selectedAnchors.Count < count)
            {
                int index = random.Next(anchorList.Length);
                selectedAnchors.Add(anchorList[index]);
            }

            return "[" + string.Join(", ", selectedAnchors) + "]";
        }


        private static readonly string[] anchorList = [
            // MATERIALS AND SUBSTANCES (1-50)
            "Gold", "Silver", "Iron", "Copper", "Lead", "Mercury", "Glass", "Salt", "Clay", "Silk",
            "Wool", "Cotton", "Paper", "Ink", "Ash", "Dust", "Sand", "Oil", "Gas", "Steam",
            "Ice", "Wax", "Resin", "Amber", "Coal", "Granite", "Marble", "Bone", "Ivory", "Leather",
            "Wood", "Cork", "Steel", "Bronze", "Rubber", "Plastic", "Cement", "Brick", "Acid", "Sugar",
            "Honey", "Milk", "Blood", "Sweat", "Tears", "Venom", "Poison", "Smoke", "Mist", "Cloud",

            // PHYSICS AND ENERGY (51-100)
            "Gravity", "Magnetism", "Static", "Friction", "Inertia", "Velocity", "Frequency", "Echo", "Shadow", "Reflection",
            "Refraction", "Prism", "Lens", "Mirror", "Spark", "Flame", "Heat", "Cold", "Vacuum", "Pressure",
            "Tension", "Vibration", "Rhythm", "Pulse", "Wave", "Current", "Signal", "Noise", "Silence", "Static",
            "Flash", "Glow", "Neon", "Laser", "Radiation", "X-ray", "Infrared", "Ultraviolet", "Spectrum", "Color",
            "Pitch", "Volume", "Weight", "Density", "Buoyancy", "Torque", "Spin", "Orbit", "Eclipse", "Tide",

            // BIOLOGY AND NATURE (101-150)
            "Root", "Leaf", "Spore", "Seed", "Pollen", "Bark", "Thorn", "Shell", "Scale", "Feather",
            "Fur", "Skin", "Muscle", "Nerve", "Spine", "Brain", "Heart", "Lung", "Stomach", "Gland",
            "Parasite", "Symbiont", "Predator", "Prey", "Hive", "Swarm", "Colony", "Nest", "Burrow", "Cocoon",
            "Mutation", "Hybrid", "Clone", "Egg", "Embryo", "Larva", "Pupa", "Fossil", "Extinction", "Evolution",
            "Scent", "Pheromone", "Vision", "Hearing", "Taste", "Touch", "Sleep", "Dream", "Hunger", "Thirst",

            // ENVIRONMENT AND GEOGRAPHY (151-200)
            "Island", "Desert", "Swamp", "Cave", "Abyss", "Peak", "Volcano", "Canyon", "Glacier", "River",
            "Ocean", "Lagoon", "Reef", "Forest", "Jungle", "Tundra", "Plateau", "Valley", "Basin", "Crater",
            "Geyser", "Waterfall", "Dune", "Shore", "Horizon", "Atmosphere", "Stratosphere", "Orbit", "Galaxy", "Nebula",
            "Meteor", "Comet", "Asteroid", "Sun", "Moon", "Star", "Planet", "Atmosphere", "Wind", "Storm",
            "Thunder", "Lightning", "Rainbow", "Dew", "Frost", "Rain", "Snow", "Hail", "Aurora", "Twilight",

            // ARCHITECTURE AND TECHNOLOGY (201-250)
            "Bridge", "Tower", "Wall", "Gate", "Door", "Key", "Lock", "Hinge", "Window", "Roof",
            "Stairs", "Tunnel", "Vault", "Dome", "Arch", "Column", "Foundation", "Scaffold", "Crane", "Gear",
            "Lever", "Pulley", "Engine", "Motor", "Turbine", "Propeller", "Wheel", "Axle", "Battery", "Wire",
            "Circuit", "Chip", "Code", "Sensor", "Antenna", "Satellite", "Rocket", "Anchor", "Chain", "Rope",
            "Net", "Trap", "Weapon", "Shield", "Armor", "Sword", "Arrow", "Compass", "Clock", "Calendar",

            // ABSTRACTIONS AND DEFINITIONS (251-300)
            "Error", "Failure", "Success", "Betrayal", "Debt", "Ritual", "Secret", "Mystery", "Legend", "Myth",
            "Chaos", "Order", "Logic", "Pattern", "Symmetry", "Paradox", "Infinity", "Void", "Purity", "Corruption",
            "Beauty", "Ugly", "Truth", "Lie", "Memory", "Oblivion", "Justice", "Crime", "Power", "Weakness",
            "Freedom", "Prison", "Gift", "Trade", "Currency", "Price", "Value", "Quality", "Quantity", "Ratio",
            "Balance", "Chaos", "Harmony", "Conflict", "Peace", "War", "Truce", "Hero", "Villain", "Ghost",

            // TIME AND MOVEMENT (301-350)
            "Instant", "Duration", "Delay", "Wait", "Start", "End", "Cycle", "Loop", "Spiral", "Zigzag",
            "Curve", "Straight", "Fast", "Slow", "Accelerate", "Brake", "Collision", "Impact", "Fall", "Jump",
            "Flight", "Swim", "Crawl", "Walk", "Run", "Dance", "Stumble", "Stillness", "Motion", "Rest",
            "Modern", "Ancient", "Future", "Past", "Era", "Decade", "Century", "Dawn", "Dusk", "Midnight",
            "Noon", "Spring", "Summer", "Autumn", "Winter", "Seasonal", "Temporary", "Eternal", "Brief", "Infinite",

            // SOCIETY AND CULTURE (351-400)
            "Language", "Alphabet", "Symbol", "Sign", "Icon", "Portrait", "Mask", "Statue", "Stage", "Theater",
            "Music", "Song", "Instrument", "Drum", "Flute", "Poem", "Book", "Map", "Chart", "Document",
            "Crown", "Throne", "Flag", "Emblem", "Border", "Treaty", "Alliance", "Rebellion", "Empire", "Nomad",
            "Village", "City", "Metropolis", "Market", "Bank", "Factory", "Hospital", "School", "Library", "Museum",
            "Kitchen", "Table", "Bed", "Chair", "Mirror", "Cup", "Plate", "Knife", "Fork", "Spoon",

            // EMOTIONS AND FEELINGS (401-450)
            "Fear", "Joy", "Anger", "Sadness", "Surprise", "Disgust", "Trust", "Envy", "Pride", "Shame",
            "Loud", "Quiet", "Bitter", "Sweet", "Sour", "Salty", "Spicy", "Smooth", "Rough", "Sharp",
            "Blunt", "Soft", "Hard", "Fragile", "Tough", "Bright", "Dim", "Dark", "Pale", "Vivid",
            "Hot", "Cold", "Lukewarm", "Heavy", "Light", "Full", "Empty", "Crowded", "Lonely", "Awkward",
            "Graceful", "Clumsy", "Strong", "Weak", "Brave", "Coward", "Smart", "Dumb", "Old", "Young",

            // OTHER (451-500)
            "Glitch", "Fragment", "Leak", "Waste", "Shadow", "Double", "Phantom", "Mimic", "Inversion", "Overlap",
            "Drift", "Shift", "Tear", "Patch", "Stitch", "Knot", "Tangle", "Web", "Maze", "Labyrinth",
            "Puzzle", "Riddle", "Clue", "Trail", "Footprint", "Stain", "Scars", "Burial", "Resurrection", "Echo",
            "Rebirth", "Survival", "Extinction", "Exile", "Refugee", "Stranger", "Guest", "Host", "Parasite", "Vessel",
            "Capsule", "Container", "Package", "Label", "Brand", "Logo", "Name", "Alias", "Secret", "Void"
        ];
    }
}
