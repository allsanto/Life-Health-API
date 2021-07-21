using Dapper;
using Life_Healthy_API.Data.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace Life_Healthy_API.Data.Repository
{
    public class FoodRepository : RepositoryBase
    {
        public FoodRepository(IConfiguration configuration)
        {
            base.configuration = configuration;
        }

        public int InsertFood_DAO(AlimentoEntity alimento)
        {
            using var db = Connection;

            var query = @"INSERT INTO alimentos
                          (descricao,
                           num_calorias,
                           data_alimento,
                           usuario_id)
                          VALUES(@Descricao,
                                 @NumCalorias,
                                 @DataAlimento,
                                 @UsuarioId);";

            return db.ExecuteScalar<int>(query, new
            {
                alimento.Descricao,
                alimento.NumCalorias,
                alimento.DataAlimento,
                alimento.UsuarioId
            });
        }

        public AlimentoEntity GetFoodById_DAO(int id)
        {
            using var db = Connection;

            var query = @"SELECT alimento_id,
	                        descricao,
	                        num_calorias,
	                        data_alimento,
	                        status,
                            usuario_id
	                        FROM alimentos
	                        WHERE alimento_id = @id";

            return db.QueryFirstOrDefault<AlimentoEntity>(query, new { id });
        }

        public IEnumerable<AlimentoEntity> GetFoodByUserId_DAO(int id)
        {
            using var db = Connection;

            var query = @"SELECT alimento_id,
	                        descricao,
	                        num_calorias,
	                        data_alimento,
	                        status,
                            usuario_id
	                        FROM alimentos
	                        WHERE usuario_id = @id
                                AND status = 1";

            return db.Query<AlimentoEntity>(query, new { id });
        }

        public int DeleteFood_DAO(int id)
        {
            using var db = Connection;

            var query = @"UPDATE alimentos
                            SET status = 2
                            WHERE alimento_id = @id";

            return db.Execute(query, new { id });
        }

        public int UpdateFood_DAO(AlimentoEntity alimento)
        {
            using var db = Connection;

            var query = @"UPDATE alimentos
                                SET descricao = @Descricao,
                                    num_calorias = @NumCalorias,
                                    data_alimento = @DataAlimento
                                WHERE alimento_id = @AlimentoId";

            return db.Execute(query, new
            {
                alimento.Descricao,
                alimento.NumCalorias,
                alimento.DataAlimento,
                alimento.AlimentoId
            });
        }
    }
}
