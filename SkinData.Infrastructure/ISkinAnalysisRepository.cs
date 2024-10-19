using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using SkinData.Domain;


namespace SkinData.Infrastructure
{
    public interface ISkinAnalysisRepository
    {
        Task<UserSkin> GetSkinDataAsync(int userId);
        Task AddSkinDataAsync(UserSkin skinAnalysis);

    }

    public class SkinAnalysisRepository : ISkinAnalysisRepository
    {

        private readonly string _connectionString;

        public SkinAnalysisRepository(IConfiguration configuration)
        {
            var skinAnalysisDbConfig = configuration.GetSection("ConnectionStrings").GetSection("SkinAnalysisDb");
            var host = skinAnalysisDbConfig["Host"];
            var port = skinAnalysisDbConfig["Port"];
            var database = skinAnalysisDbConfig["Database"];
            var username = skinAnalysisDbConfig["Username"];
            var password = skinAnalysisDbConfig["Password"];

            _connectionString = $"Host={host};Port={port};Database={database};Username={username};Password={password};";
        }

        public async Task<UserSkin> GetSkinDataAsync(int userId)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new NpgsqlCommand("SELECT * FROM user_skin WHERE user_id = @UserId", connection);
                command.Parameters.AddWithValue("UserId", userId);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        var skin = new UserSkin
                        {
                            UserId = reader.GetInt64(reader.GetOrdinal("user_id")),

                            // Pores Left Cheek
                            PoresLeftCheekConfidence = reader.GetDecimal(reader.GetOrdinal("pores_left_cheek_confidence")),
                            PoresLeftCheekValue = reader.GetInt32(reader.GetOrdinal("pores_left_cheek_value")),

                            // Nasolabial Fold
                            NasolabialFoldConfidence = reader.GetDecimal(reader.GetOrdinal("nasolabial_fold_confidence")),
                            NasolabialFoldValue = reader.GetInt32(reader.GetOrdinal("nasolabial_fold_value")),

                            // Eye Pouch
                            EyePouchConfidence = reader.GetDecimal(reader.GetOrdinal("eye_pouch_confidence")),
                            EyePouchValue = reader.GetInt32(reader.GetOrdinal("eye_pouch_value")),

                            // Forehead Wrinkle
                            ForeheadWrinkleConfidence = reader.GetDecimal(reader.GetOrdinal("forehead_wrinkle_confidence")),
                            ForeheadWrinkleValue = reader.GetInt32(reader.GetOrdinal("forehead_wrinkle_value")),

                            // Skin Spot
                            SkinSpotConfidence = reader.GetDecimal(reader.GetOrdinal("skin_spot_confidence")),
                            SkinSpotValue = reader.GetInt32(reader.GetOrdinal("skin_spot_value")),

                            // Acne
                            AcneConfidence = reader.GetDecimal(reader.GetOrdinal("acne_confidence")),
                            AcneValue = reader.GetInt32(reader.GetOrdinal("acne_value")),

                            // Pores Forehead
                            PoresForeheadConfidence = reader.GetDecimal(reader.GetOrdinal("pores_forehead_confidence")),
                            PoresForeheadValue = reader.GetInt32(reader.GetOrdinal("pores_forehead_value")),

                            // Pores Jaw
                            PoresJawConfidence = reader.GetDecimal(reader.GetOrdinal("pores_jaw_confidence")),
                            PoresJawValue = reader.GetInt32(reader.GetOrdinal("pores_jaw_value")),

                            // Left Eyelids
                            LeftEyelidsConfidence = reader.GetDecimal(reader.GetOrdinal("left_eyelids_confidence")),
                            LeftEyelidsValue = reader.GetInt32(reader.GetOrdinal("left_eyelids_value")),

                            // Eye Finelines
                            EyeFinelinesConfidence = reader.GetDecimal(reader.GetOrdinal("eye_finelines_confidence")),
                            EyeFinelinesValue = reader.GetInt32(reader.GetOrdinal("eye_finelines_value")),

                            // Dark Circle
                            DarkCircleConfidence = reader.GetDecimal(reader.GetOrdinal("dark_circle_confidence")),
                            DarkCircleValue = reader.GetInt32(reader.GetOrdinal("dark_circle_value")),

                            // Crow's Feet
                            CrowsFeetConfidence = reader.GetDecimal(reader.GetOrdinal("crows_feet_confidence")),
                            CrowsFeetValue = reader.GetInt32(reader.GetOrdinal("crows_feet_value")),

                            // Pores Right Cheek
                            PoresRightCheekConfidence = reader.GetDecimal(reader.GetOrdinal("pores_right_cheek_confidence")),
                            PoresRightCheekValue = reader.GetInt32(reader.GetOrdinal("pores_right_cheek_value")),

                            // Blackhead
                            BlackheadConfidence = reader.GetDecimal(reader.GetOrdinal("blackhead_confidence")),
                            BlackheadValue = reader.GetInt32(reader.GetOrdinal("blackhead_value")),

                            // Glabella Wrinkle
                            GlabellaWrinkleConfidence = reader.GetDecimal(reader.GetOrdinal("glabella_wrinkle_confidence")),
                            GlabellaWrinkleValue = reader.GetInt32(reader.GetOrdinal("glabella_wrinkle_value")),

                            // Mole
                            MoleConfidence = reader.GetDecimal(reader.GetOrdinal("mole_confidence")),
                            MoleValue = reader.GetInt32(reader.GetOrdinal("mole_value")),

                            // Right Eyelids
                            RightEyelidsConfidence = reader.GetDecimal(reader.GetOrdinal("right_eyelids_confidence")),
                            RightEyelidsValue = reader.GetInt32(reader.GetOrdinal("right_eyelids_value")),

                            // Skin Type
                            SkinType = reader.GetInt32(reader.GetOrdinal("skin_type")),

                            // Skin Type Detail 0
                            SkinTypeDetail0Confidence = reader.GetDecimal(reader.GetOrdinal("skin_type_detail_0_confidence")),
                            SkinTypeDetail0Value = reader.GetInt32(reader.GetOrdinal("skin_type_detail_0_value")),

                            // Skin Type Detail 1
                            SkinTypeDetail1Confidence = reader.GetDecimal(reader.GetOrdinal("skin_type_detail_1_confidence")),
                            SkinTypeDetail1Value = reader.GetInt32(reader.GetOrdinal("skin_type_detail_1_value")),

                            // Skin Type Detail 2
                            SkinTypeDetail2Confidence = reader.GetDecimal(reader.GetOrdinal("skin_type_detail_2_confidence")),
                            SkinTypeDetail2Value = reader.GetInt32(reader.GetOrdinal("skin_type_detail_2_value")),

                            // Skin Type Detail 3
                            SkinTypeDetail3Confidence = reader.GetDecimal(reader.GetOrdinal("skin_type_detail_3_confidence")),
                            SkinTypeDetail3Value = reader.GetInt32(reader.GetOrdinal("skin_type_detail_3_value"))
                        };

                        return skin;
                    }
                }
                return null;
            }
        }

        public async Task AddSkinDataAsync(UserSkin skinAnalysis)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                var query = @"
            INSERT INTO public.user_skin (
                user_id,
                pores_left_cheek_confidence, pores_left_cheek_value,
                nasolabial_fold_confidence, nasolabial_fold_value,
                eye_pouch_confidence, eye_pouch_value,
                forehead_wrinkle_confidence, forehead_wrinkle_value,
                skin_spot_confidence, skin_spot_value,
                acne_confidence, acne_value,
                pores_forehead_confidence, pores_forehead_value,
                pores_jaw_confidence, pores_jaw_value,
                left_eyelids_confidence, left_eyelids_value,
                eye_finelines_confidence, eye_finelines_value,
                dark_circle_confidence, dark_circle_value,
                crows_feet_confidence, crows_feet_value,
                pores_right_cheek_confidence, pores_right_cheek_value,
                blackhead_confidence, blackhead_value,
                glabella_wrinkle_confidence, glabella_wrinkle_value,
                mole_confidence, mole_value,
                right_eyelids_confidence, right_eyelids_value,
                skin_type,
                skin_type_detail_0_confidence, skin_type_detail_0_value,
                skin_type_detail_1_confidence, skin_type_detail_1_value,
                skin_type_detail_2_confidence, skin_type_detail_2_value,
                skin_type_detail_3_confidence, skin_type_detail_3_value
            )
            VALUES (
                @UserId,
                @PoresLeftCheekConfidence, @PoresLeftCheekValue,
                @NasolabialFoldConfidence, @NasolabialFoldValue,
                @EyePouchConfidence, @EyePouchValue,
                @ForeheadWrinkleConfidence, @ForeheadWrinkleValue,
                @SkinSpotConfidence, @SkinSpotValue,
                @AcneConfidence, @AcneValue,
                @PoresForeheadConfidence, @PoresForeheadValue,
                @PoresJawConfidence, @PoresJawValue,
                @LeftEyelidsConfidence, @LeftEyelidsValue,
                @EyeFinelinesConfidence, @EyeFinelinesValue,
                @DarkCircleConfidence, @DarkCircleValue,
                @CrowsFeetConfidence, @CrowsFeetValue,
                @PoresRightCheekConfidence, @PoresRightCheekValue,
                @BlackheadConfidence, @BlackheadValue,
                @GlabellaWrinkleConfidence, @GlabellaWrinkleValue,
                @MoleConfidence, @MoleValue,
                @RightEyelidsConfidence, @RightEyelidsValue,
                @SkinType,
                @SkinTypeDetail0Confidence, @SkinTypeDetail0Value,
                @SkinTypeDetail1Confidence, @SkinTypeDetail1Value,
                @SkinTypeDetail2Confidence, @SkinTypeDetail2Value,
                @SkinTypeDetail3Confidence, @SkinTypeDetail3Value
            )";

                var parameters = new
                {
                    skinAnalysis.UserId,
                    skinAnalysis.PoresLeftCheekConfidence,
                    skinAnalysis.PoresLeftCheekValue,
                    skinAnalysis.NasolabialFoldConfidence,
                    skinAnalysis.NasolabialFoldValue,
                    skinAnalysis.EyePouchConfidence,
                    skinAnalysis.EyePouchValue,
                    skinAnalysis.ForeheadWrinkleConfidence,
                    skinAnalysis.ForeheadWrinkleValue,
                    skinAnalysis.SkinSpotConfidence,
                    skinAnalysis.SkinSpotValue,
                    skinAnalysis.AcneConfidence,
                    skinAnalysis.AcneValue,
                    skinAnalysis.PoresForeheadConfidence,
                    skinAnalysis.PoresForeheadValue,
                    skinAnalysis.PoresJawConfidence,
                    skinAnalysis.PoresJawValue,
                    skinAnalysis.LeftEyelidsConfidence,
                    skinAnalysis.LeftEyelidsValue,
                    skinAnalysis.EyeFinelinesConfidence,
                    skinAnalysis.EyeFinelinesValue,
                    skinAnalysis.DarkCircleConfidence,
                    skinAnalysis.DarkCircleValue,
                    skinAnalysis.CrowsFeetConfidence,
                    skinAnalysis.CrowsFeetValue,
                    skinAnalysis.PoresRightCheekConfidence,
                    skinAnalysis.PoresRightCheekValue,
                    skinAnalysis.BlackheadConfidence,
                    skinAnalysis.BlackheadValue,
                    skinAnalysis.GlabellaWrinkleConfidence,
                    skinAnalysis.GlabellaWrinkleValue,
                    skinAnalysis.MoleConfidence,
                    skinAnalysis.MoleValue,
                    skinAnalysis.RightEyelidsConfidence,
                    skinAnalysis.RightEyelidsValue,
                    skinAnalysis.SkinType,
                    skinAnalysis.SkinTypeDetail0Confidence,
                    skinAnalysis.SkinTypeDetail0Value,
                    skinAnalysis.SkinTypeDetail1Confidence,
                    skinAnalysis.SkinTypeDetail1Value,
                    skinAnalysis.SkinTypeDetail2Confidence,
                    skinAnalysis.SkinTypeDetail2Value,
                    skinAnalysis.SkinTypeDetail3Confidence,
                    skinAnalysis.SkinTypeDetail3Value
                };

                await connection.ExecuteAsync(query, parameters);
            }
        }


    }
}
