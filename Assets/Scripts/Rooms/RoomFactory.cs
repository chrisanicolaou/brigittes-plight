using System.Collections.Generic;
using System.Linq;
using ChiciStudios.BrigittesPlight.Acts;
using ChiciStudios.BrigittesPlight.Encounters;
using UnityEngine;

namespace ChiciStudios.BrigittesPlight.Rooms
{
    public static class RoomFactory
    {
        public static RoomContext CreateRoomContext(RoomModel roomModel, ActContext actContext)
        {
            // CONVERT RANDOM TO SeedContext AT SOME POINT PLS
            var random = new System.Random();

            // Setup the returning room context
            var roomContext = new RoomContext
            {
                RoomName = roomModel.Name,
                CurrentEncounterIndex = 0,
                EventLuck = roomModel.EventLuck,
                TotalEncounters = roomModel.TotalEncounters,
                Encounters = new Encounter[roomModel.TotalEncounters]
            };
            
            // Begin generating the room encounters
            var remainingInns = roomModel.InnCount;
            var remainingMerchants = roomModel.MerchantCount;
            var remainingEvents = roomModel.EventCount;
            var remainingEncountersToGenerate = roomModel.TotalEncounters - 1;
            
            // Create the final (and potentially penultimate) encounter first as they are treated separately and may influence
            // the rest of the generation
            roomContext.Encounters[^1] = actContext.IsFinalRoom ? CreateActBossEncounter(actContext) : CreateNextRoomEncounter(actContext);

            if (actContext.IsFinalRoom)
            {
                roomContext.Encounters[^2] = CreateInnMerchantEncounter(roomContext);
                remainingEncountersToGenerate--;
                remainingMerchants = Mathf.Max(0, remainingMerchants - 1);
                remainingInns = Mathf.Max(0, remainingInns - 1);
            }
            else if (roomModel is MinibossRoomModel minibossRoomModel)
            {
                roomContext.Encounters[^2] = CreateMinibossEncounter(minibossRoomModel);
                remainingEncountersToGenerate--;
            }
            
            // Of the remaining encounters, create empty encounters
            for (var i = 0; i < remainingEncountersToGenerate; i++)
            {
                roomContext.Encounters[i] = new Encounter();
                roomContext.Encounters[i].Options = new EncounterOption[3];
            }

            // Of the remaining inns, randomly populate encounters with inns
            var innFreeIndexes = Enumerable.Range(0, remainingEncountersToGenerate).ToList();
            for (var i = 0; i < remainingInns; i++)
            {
                if (innFreeIndexes.Count == 0) break;
                // Pull a random index from the encounters not populated with an inn
                var randomIndex = innFreeIndexes[random.Next(0, innFreeIndexes.Count)];
                
                // Find an empty slot on that encounter's options (to make sure not to override existing encounter options)
                var encounterOpts = roomContext.Encounters[randomIndex].Options;
                for (var j = 0; j < 3; j++)
                {
                    if (encounterOpts[j] != null) continue;
                    
                    // When encounters are complete, this needs to be changed from "Name" to whatever encounters actually are!
                    encounterOpts[j] = new EncounterOption
                    {
                        Name = "Inn"
                    };
                    break;
                }

                innFreeIndexes.Remove(randomIndex);
            }

            // Repeat of the above for merchants
            var merchantFreeIndexes = Enumerable.Range(0, remainingEncountersToGenerate).ToList();
            for (var i = 0; i < remainingMerchants; i++)
            {
                // Pull a random index from the encounters not populated with an inn
                var randomIndex = random.Next(0, merchantFreeIndexes.Count);
                
                // Find an empty slot on that encounter's options (to make sure not to override existing encounter options)
                var encounterOpts = roomContext.Encounters[randomIndex].Options;
                for (var j = 0; j < 3; j++)
                {
                    if (encounterOpts[j] != null) continue;
                    
                    // When encounters are complete, this needs to be changed from "Name" to whatever encounters actually are!
                    encounterOpts[j] = new EncounterOption
                    {
                        Name = "Merchant"
                    };
                    break;
                }

                innFreeIndexes.Remove(randomIndex);
            }

            // Repeat of the above for events
            var eventFreeIndexes = Enumerable.Range(0, remainingEncountersToGenerate).ToList();
            for (var i = 0; i < remainingEvents; i++)
            {
                // Pull a random index from the encounters not populated with an inn
                var randomIndex = random.Next(0, eventFreeIndexes.Count);
                
                // Find an empty slot on that encounter's options (to make sure not to override existing encounter options)
                var encounterOpts = roomContext.Encounters[randomIndex].Options;
                for (var j = 0; j < 3; j++)
                {
                    if (encounterOpts[j] != null) continue;
                    
                    // When encounters are complete, this needs to be changed from "Name" to whatever encounters actually are!
                    encounterOpts[j] = new EncounterOption
                    {
                        Name = "Event"
                    };
                    break;
                }

                innFreeIndexes.Remove(randomIndex);
            }
            
            // Now loop over the remaining encounters to generate and populate any nulls with enemies
            for (var i = 0; i < remainingEncountersToGenerate; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    if (roomContext.Encounters[i].Options[j] != null) continue;
                    roomContext.Encounters[i].Options[j] = new EncounterOption
                    {
                        Name = "Enemy!"
                    };
                }
            }

            return roomContext;
        }

        private static Encounter CreateInnMerchantEncounter(RoomContext roomContext)
        {
            var encounter = new Encounter
            {
                Options = new EncounterOption[2]
            };
            encounter.Options[0] = new EncounterOption { Name = "Inn" };
            encounter.Options[1] = new EncounterOption { Name = "Merchant" };

            return encounter;
        }

        private static Encounter CreateMinibossEncounter(MinibossRoomModel minibossRoomModel)
        {
            var encounter = new Encounter
            {
                Options = new EncounterOption[1]
            };
            encounter.Options[0] = new EncounterOption { Name = "Miniboss!" };

            return encounter;
        }

        private static Encounter CreateNextRoomEncounter(ActContext actContext)
        {
            var encounter = new Encounter
            {
                Options = new EncounterOption[3]
            };
            encounter.Options[0] = new EncounterOption { Name = "Room Option 1" };
            encounter.Options[1] = new EncounterOption { Name = "Room Option 2" };
            encounter.Options[2] = new EncounterOption { Name = "Room Option 3" };

            return encounter;
        }

        private static Encounter CreateActBossEncounter(ActContext actContext)
        {
            var encounter = new Encounter
            {
                Options = new EncounterOption[1]
            };
            encounter.Options[0] = new EncounterOption { Name = "ACT BOSS!" };

            return encounter;
        }
    }
}